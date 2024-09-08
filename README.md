# UrbIA - Urban Insights Atlas

## Overview
*UrbIA* (Urban Insights Atlas) is a GovHack 2024 project developed by the Banico Family. The full project concept is a geospatial data platform powered by the Digital Atlas of Australia and AI, designed to make urban data more accessible and empower decision-making. *UrbIA* allows users to explore local environments, receive AI-generated insights, and contribute their own suggestions, with a focus on road safety, capacity forecasting, and sustainable urban development.

## Features (of the fully realised vision of UrbIA)
- **Geospatial Navigation**: Users can navigate the map using the Digital Atlas of Australia to explore data on road safety, traffic patterns, public transport usage, and more.
- **Integration with the Digital Atlas of Australia**: Data is retrieved from the Digital Atlas of Australia API (https://digital.atlas.gov.au/api/search/definition/) for geolocated data input.
- **AI-Generated Insights**: *UrbIA* utilizes AI to provide actionable suggestions based on real-time data. These insights cover areas such as road safety improvements, infrastructure capacity needs, and sustainability measures.
- **Crowdsourced Feedback**: Users can react to AI-generated insights by rating and offering their own suggestions, creating a collaborative environment for improving urban spaces.
  
## Use Cases
- **Road Safety**: By analyzing Victorian Road Crash Data, *UrbIA* generates safety suggestions such as adding guardrails or improving road lighting.
- **Capacity Forecasting**: Using traffic volume data, the platform offers insights on where capacity upgrades or maintenance may be needed.
- **Sustainability**: *UrbIA* provides AI-driven recommendations for expanding bike paths, optimizing public transport routes, and promoting greener mobility options.

## How It Works
1. **Data Collection**: Geolocated data is sourced from the Digital Atlas of Australia and other public datasets such as road crash statistics, traffic volume reports, and public service utilization.
2. **AI Analysis**: OpenAI is used to analyze this data and generate insights. For example, AI may suggest infrastructure improvements based on accident hotspots or low traffic volumes.
3. **User Interaction**: Users can view these insights, rate them, and provide feedback, fostering a collaborative approach to urban planning.

## Proof-of-Concept Code
This code is for the proof-of-concept for generating insights from geolocated open data. Raw data used can be found in the "/data-sources" folder. For use in this proof-of-concept data is pre-processed manually to match formats (geojson) and reduce size. Preprocessed data files can be found in the "src/Input Data" folder.

The generated insights can be found in "src/Output Data" folder.

## Setup
You will need to create an Azure OpenAI Account. You can learn more about it here https://learn.microsoft.com/en-us/azure/ai-services/openai/overview.

In src folder, copy appsettings.json.setup to appsettings.json, and update the following with values from Azure OpenAI.
- AZURE_OPENAI_ENDPOINT
- AZURE_OPENAI_KEY
- AZURE_OPENAI_DEPLOYMENT

## Run
- Using command or terminal, go to src folder.
- Update archive.csv file with your input image id, title, and urls.
- Ensure you have dotnet. If not, download dotnet at https://dotnet.microsoft.com/en-us/download.
- dotnet build
- dotnet run
