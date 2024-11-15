import streamlit as st
import plotly.express as px
import pandas as pd
from streamlit_tree_select import tree_select
from data_loading import load_measurements, load_assets, load_signals
from models import Asset, Signal


# Page config
st.set_page_config(
    page_title="Time Series Viewer",
    page_icon="📈",
    layout="wide"
)

# Title
st.title("Time Series Viewer")

# Load data
@st.cache_data
def cached_load_data(signals: list[int]) -> pd.DataFrame:
    try:
        data_df = load_measurements()
        return data_df[
            data_df["SignalId"].isin(signals)
        ].sort_values(by="Ts")
    except Exception as e:
        st.error(e)
        return pd.DataFrame()

@st.cache_data
def assets() -> list[Asset]:
    return load_assets()

@st.cache_data
def signals() -> list[Signal]:
    return load_signals()

@st.cache_data
def tree_data() -> list[dict]:
    return [
        {
            "label": str(asset),
            "value": asset.id,
            "children": [
                {
                    "label": str(signal),
                    "value": signal.id
                }
                for signal in signals()
                if signal.asset_id == asset.id
            ]
        }
        for asset in assets()
    ]

# Container for selection and map
with st.container():
    # Create two columns
    col1, col2 = st.columns([1, 1])
    
    with col2:
        # Signal selection
        st.subheader("Signal Selection")
        st.caption("Select signals to display on the plot")
        return_select = tree_select(tree_data(), check_model="leaf")
        selected_signal_ids = [
            int(value)
            for value in return_select['checked']
        ]
        selected_asset_ids = list(set([
            signal.asset_id
            for signal in signals()
            if signal.id in selected_signal_ids
        ]))

    with col1:
        # Map of assets
        st.subheader("Asset Locations")
        map_data = pd.DataFrame(
            [(
                asset.latitude,
                asset.longitude,
                '#ff0000' if asset.id in selected_asset_ids else '#0000ff'
            ) for asset in assets()],
            columns=['latitude', 'longitude', 'color']
        )
        st.map(
            data=map_data,
            latitude='latitude',
            longitude='longitude',
            color='color'
        )


if not selected_signal_ids:
    st.warning("Please select at least one signal to display data.")
    st.stop()

# Filter data based on selection
selected_signals = [signal for signal in signals() if signal.id in selected_signal_ids]
filtered_df = cached_load_data(selected_signal_ids)

# Plot
if filtered_df.empty:
    st.warning(f"No data available for selected signals: {', '.join([signal.name for signal in selected_signals])}")
else:
    fig = px.line(
        filtered_df,
        x="Ts",
        y="MeasurementValue",
        color="SignalId",
        title="Time Series Data",
        labels={
            "Ts": "Timestamp",
            "MeasurementValue": "Value"
        },
    )
    fig.for_each_trace(lambda t: t.update(name = next(
        f"{signal.name} ({signal.unit})" for signal in selected_signals if signal.id == int(t.name)
    )))
    fig.update_layout(legend=dict(orientation="h", yanchor="bottom", y=1.02, xanchor="center", x=0.5))
    st.plotly_chart(fig, use_container_width=True)
    
    # Export button
    with st.expander("Export Figure"):
        file_name = st.text_input("Enter file name", value="time_series")
        file_format = st.selectbox("Select file format", options=["pdf", "png", "jpeg", "svg"])
        st.download_button(
            label="Download",
            data=fig.to_image(format=file_format),
            file_name=f"{file_name}.{file_format}"
        )