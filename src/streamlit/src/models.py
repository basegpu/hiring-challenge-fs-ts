from pydantic import BaseModel, Field
from uuid import uuid4


class Asset(BaseModel):
    id: int = Field(..., alias="AssetID", description="The unique identifier for the asset")
    latitude: float = Field(..., alias="Latitude", description="The latitude of the asset")
    longitude: float = Field(..., alias="Longitude", description="The longitude of the asset")
    description: str = Field(..., alias="descri", description="The description of the asset")

    def __str__(self):
        return f"{self.id} - {self.description}"


class Signal(BaseModel):
    id: int = Field(..., alias="SignalId", description="The unique identifier for the signal")
    guid: str = Field(..., alias="SignalGId", description="The GUID of the signal")
    name: str = Field(..., alias="SignalName", description="The name of the signal")
    asset_id: int = Field(..., alias="AssetId", description="The unique identifier for the asset")
    unit: str = Field(..., alias="Unit", description="The unit of the signal")

    def __str__(self):
        return f"{self.name} ({self.unit})"