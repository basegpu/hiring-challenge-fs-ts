import json
import pandas as pd
from pathlib import Path
import requests
from typing import List, TypeVar, Type
from models import Asset, Signal, SignalData
from settings import settings


T = TypeVar('T', Asset, Signal)

class DataProvider:
    def assets(self) -> List[Asset]:
        """
        Load assets from data source
        
        Returns:
            List[Asset]
        """
        raise NotImplementedError

    def signals(self) -> List[Signal]:
        """
        Load signals from data source
        
        Returns:
            List[Signal]
        """
        raise NotImplementedError
    
    def signal_data(self, signal_id: int) -> SignalData:
        """
        Load signal data (measurements by column)from data source for a given signal id.
        
        Returns:
            SignalData
        """
        raise NotImplementedError


class LocalDataProvider(DataProvider):
    _data_dir: Path

    def __init__(self, data_path: str):
        self._data_dir = Path(data_path)

    def load_json_data(self, model_class: Type[T], filename: str) -> List[T]:
        """
        Generic function to load and parse JSON data into model instances
        
        Args:
            model_class: The class to instantiate (Asset or Signal)
            filename: Name of the JSON file to load
            
        Returns:
            List of model instances
        """
        file_path = self._data_dir / filename
        try:
            with open(file_path, 'r') as f:
                data = json.load(f)
        except Exception as e:
            raise Exception(f"Error loading data: {e}")
        
        return [model_class(**item) for item in data]


    def assets(self) -> List[Asset]:
        """Load assets from JSON file"""
        return self.load_json_data(Asset, "assets.json")


    def signals(self) -> List[Signal]:
        """Load signals from JSON file"""
        return self.load_json_data(Signal, "signals.json")


    def load_csv_data(self, filename: str, delimiter: str = ",") -> pd.DataFrame:
        """Load data from CSV file"""
        try:
            return pd.read_csv(self._data_dir / filename, delimiter=delimiter, decimal=",")
        except Exception as e:
            raise Exception(f"Error loading data: {e}")
    
    def signal_data(self, signal_id: int) -> SignalData:
        """Load signal data (measurements by column) from CSV file"""
        data_df = self.load_csv_data("measurements.csv", delimiter="|")
        filter = data_df["SignalId"] == signal_id
        return SignalData(
            signal_id=signal_id, 
            timestamps=data_df[filter]["Ts"].tolist(), 
            values=data_df[filter]["MeasurementValue"].tolist()
        )


class RemoteDataProvider(DataProvider):
    _data_host: str

    def __init__(self, data_host: str):
        self._data_host = data_host
        response = requests.get(f"{self._data_host}/api/health")
        if response.status_code != 200:
            raise Exception(f"Health check failed with status code {response.status_code}")
    
    def _make_request(self, endpoint: str) -> list[dict]:
        """Make a request to the API and return the JSON response"""
        response = requests.get(f"{self._data_host}/api/{endpoint}")
        if response.status_code != 200:
            raise Exception(f"Error loading {endpoint}: {response.status_code}")
        return response.json()
    
    def _make_typed_request(self, endpoint: str, model_class: Type[T]) -> List[T]:
        """Make a request to the API and return a list of typed objects"""
        data = self._make_request(endpoint)
        return [model_class(**item) for item in data]

    def assets(self) -> List[Asset]:
        """Load assets from API"""
        return self._make_typed_request("assets", Asset)
    
    def signals(self) -> List[Signal]:
        """Load signals from API"""
        return self._make_typed_request("signals", Signal)
    
    def signal_data(self, signal_id: int) -> SignalData:
        """Load signal data (measurements by column) from API"""
        return SignalData(**self._make_request(f"data?signalId={signal_id}&columns=true"))


class DataProviderFactory:
    @staticmethod
    def make() -> DataProvider:
        if settings.data_provider.lower() == "remote":
            return RemoteDataProvider(data_host=settings.data_host)
        else:
            return LocalDataProvider(data_path=settings.data_path)
