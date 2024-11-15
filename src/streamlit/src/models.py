from datetime import datetime
from pydantic import BaseModel, Field, AliasChoices


class Asset(BaseModel):
    id: int = Field(..., validation_alias=AliasChoices("AssetID", "id"), description="The unique identifier for the asset")
    latitude: float = Field(..., validation_alias=AliasChoices("Latitude", "latitude"), description="The latitude of the asset")
    longitude: float = Field(..., validation_alias=AliasChoices("Longitude", "longitude"), description="The longitude of the asset")
    description: str = Field(..., validation_alias=AliasChoices("descri", "description"), description="The description of the asset")

    def __str__(self):
        return f"{self.id} - {self.description}"


class Signal(BaseModel):
    id: int = Field(..., validation_alias=AliasChoices("SignalId", "id"), description="The unique identifier for the signal")
    guid: str = Field(..., validation_alias=AliasChoices("SignalGId", "guid"), description="The GUID of the signal")
    name: str = Field(..., validation_alias=AliasChoices("SignalName", "name"), description="The name of the signal")
    asset_id: int = Field(..., validation_alias=AliasChoices("AssetId", "assetId"), description="The unique identifier for the asset")
    unit: str = Field(..., validation_alias=AliasChoices("Unit", "unit"), description="The unit of the signal")

    def __str__(self):
        return f"{self.name} ({self.unit})"


class Measurement(BaseModel):
    signal_id: int = Field(..., validation_alias=AliasChoices("SignalId", "signalId"), description="The unique identifier for the signal")
    timestamp: datetime = Field(..., validation_alias=AliasChoices("Ts", "timestamp"), description="The timestamp of the measurement taken")
    value: float = Field(..., validation_alias=AliasChoices("MeasurementValue", "value"), description="The value of the measurement")

    def __str__(self):
        return f"{self.timestamp} - {self.value}"
