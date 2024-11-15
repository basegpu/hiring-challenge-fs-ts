import streamlit as st
import plotly.express as px
import pandas as pd
from data_loading import load_measurements, load_assets, load_signals, Signal


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
    
# Sidebar controls
with st.sidebar:
    st.header("Controls")
    
    # Asset selection
    selected_assets = st.multiselect(
        "Select Assets",
        options=load_assets()
    )

    signal_for_assets = [
        signal for signal in load_signals()
        if signal.asset_id in [asset.id for asset in selected_assets]
    ]
    
    # Signal selection
    selected_signals = st.multiselect(
        "Select Signals",
        options=signal_for_assets
    )

if not selected_signals:
    st.warning("Please select at least one signal to display data.")
    st.stop()

# Filter data based on selection
filtered_df = cached_load_data([signal.id for signal in selected_signals])

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