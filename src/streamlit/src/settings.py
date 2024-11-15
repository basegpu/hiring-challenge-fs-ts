from pydantic import Field
from pydantic_settings import BaseSettings

class Settings(BaseSettings):
    data_provider: str = Field(default="local", description="Data provider to use")
    data_path: str = Field(default="./data", description="Path to data directory")
    data_host: str = Field(default="http://localhost:8080", description="Host for remote data provider")

settings = Settings()
