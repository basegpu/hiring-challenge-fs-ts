from datetime import datetime
from pydantic import BaseModel, Field, AliasChoices
from typing import List

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


class SignalData(BaseModel):
    signal_id: int = Field(..., validation_alias=AliasChoices("signal_id", "signalId"), description="The unique identifier for the signal")
    timestamps: List[datetime] = Field(..., description="The timestamps for the signal")
    values: List[float] = Field(..., description="The values for the signal")

    class Config:
        populate_by_name = True

    def __str__(self):
        return f"{self.signal.name} - {self.signal.unit}"
