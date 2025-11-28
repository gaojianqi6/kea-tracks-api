# 🦜 Project Vision: KeaTracks

KeaTracks is a data-driven hiking companion that aggregates official NZ Department of Conservation (DOC) data and enhances it with Generative AI and Community insights.

**Primary Goal:** To demonstrate "Senior-level" Full Stack capability by combining Geospatial Data, Background Logic, AI, and SaaS-like features (PDF Generation).

## 1. Core Functional Pillars

### 🟢 Pillar A: Discovery (The "Search Engine")

**Geolocation Home:** The app must ask for browser location permission.

**Logic:** If allowed, calculate distance to tracks. If denied, default to Hamilton.

**Structured Filtering:**

*   **Region:** Dropdown (e.g., Waikato, Bay of Plenty)
*   **Difficulty:** Checkbox (Easiest, Easy, Intermediate, Advanced)
*   **Duration:** Slider (1h to 8h+)
*   **Dog Access:** Toggle switch (Crucial for NZ dog owners)

**Map Interface:** A visual map showing pins of the search results.

### 🟢 Pillar B: Kea Scout (The "AI Agent")

**Natural Language Input:** Users type: *"I want a romantic sunset walk near Raglan."*

**RAG-Lite Logic:**

1.  AI analyzes the text to find keywords (Sunset, Raglan)
2.  System searches the database for matches
3.  AI summarizes the results into a friendly suggestion

**Weather Integration:** Before showing the result, the system checks the weather forecast for that specific track's coordinates.

### 🟢 Pillar C: Safety (The "Monetization")

**Offline Safety Pack:** A button on the Track Detail page.

*   **Function:** Generates a PDF on the server
*   **Content:** Static Map image, Emergency contacts, Elevation profile, Recent Alerts

**Live Alerts:** Display "Warning Banners" if the DOC API reports a track closure.

### 🟢 Pillar D: Community (The "Social Proof")

*   **User Profiles:** Users can set a nickname and avatar (via Clerk)
*   **Micro-Reviews:** Users can leave a star rating and a short comment (< 280 chars) on a track
*   **Gamification:** Users earn a "Verified Hiker" badge after 3 reviews

## 2. Data Strategy

### Sources

*   **DOC Open API:** The "Source of Truth" for Track Name, Geometry (Location), and Alerts
*   **OpenAI:** Used for summarizing descriptions and interpreting user chat
*   **User Generated:** Reviews and Ratings

### Data Flow

*   **Ingestion:** A background service runs every 24 hours to pull data from DOC
*   **Storage:** Data is stored in our PostgreSQL database. We never query the DOC API in real-time when a user searches (too slow). We query our database.
