from pydantic import Field
from pydantic_settings import BaseSettings

class Settings(BaseSettings):
    data_provider: str = Field(default="local", description="Data provider to use", lower=True)
    data_path: str = Field(default="./data", description="Path to data directory")

settings = Settings()
