import json
from pathlib import Path
from typing import List, TypeVar, Type, Tuple
from models import Asset, Signal
import pandas as pd


T = TypeVar('T', Asset, Signal)
DATA_DIR = Path(__file__).parent.parent / "data"


def load_json_data(model_class: Type[T], filename: str) -> List[T]:
    """
    Generic function to load and parse JSON data into model instances
    
    Args:
        model_class: The class to instantiate (Asset or Signal)
        filename: Name of the JSON file to load
        
    Returns:
        List of model instances
    """
    try:
        file_path = DATA_DIR / filename
        
        with open(file_path, 'r') as f:
            data = json.load(f)
    except Exception as e:
        raise Exception(f"Error loading data: {e}")
    
    return [model_class(**item) for item in data]


def load_assets() -> List[Asset]:
    """Load assets from JSON file"""
    return load_json_data(Asset, "assets.json")


def load_signals() -> List[Signal]:
    """Load signals from JSON file"""
    return load_json_data(Signal, "signals.json")


def load_csv_data(filename: str, delimiter: str = ",") -> pd.DataFrame:
    """
    Load data from CSV file.

    Returns:
        pd.DataFrame: DataFrame containing the loaded data
    """
    try:
        return pd.read_csv(DATA_DIR / filename, delimiter=delimiter, decimal=",")
    except Exception as e:
        raise Exception(f"Error loading data: {e}")

def load_measurements() -> pd.DataFrame:
    """
    Load measurements data from CSV file
    
    Returns:
        pd.DataFrame: DataFrame containing the loaded measurements data (timestamp, signal id, value)
    """
    return load_csv_data("measurements.csv", delimiter="|")