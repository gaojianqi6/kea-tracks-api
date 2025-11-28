# 🦜 Project Proposal: KeaTracks
**Tagline:** Hike Smarter. Explore Safer. Your intelligent companion for the New Zealand outdoors.

---

## 1. The Story & Background
New Zealand is world-famous for its stunning walks and hikes (Tramping). However, for both international tourists and locals, finding the *right* track can be frustrating and dangerous.

**The Problem:**
*   **Information Overload:** The official Department of Conservation (DOC) data is accurate but hard to search. Finding a "dog-friendly, easy walk with a waterfall near Hamilton" requires opening 10 different tabs.
*   **Hidden Dangers:** Critical safety alerts (flooded bridges, track closures) are often buried in long text descriptions.
*   **No Signal:** Once hikers enter the bush, they lose internet connection. If they haven't prepared physical maps, they are at risk of getting lost.

**The Opportunity:**
New Zealand needs a modern, intelligent platform that bridges the gap between official government data and the user experience of a modern travel app.

---

## 2. The Solution: KeaTracks
**KeaTracks** is a smart web platform that acts as a personal outdoor guide. It aggregates official government data and uses Artificial Intelligence to personalize the experience, ensuring users find the perfect track and get home safely.

It turns a complex database of thousands of tracks into a simple conversation: *"Where should I go today?"*

---

## 3. Core Functions (What it does)

### 🟢 1. Smart Discovery ("The Compass")
*   **Instant Local Recommendations:** As soon as the user opens the app, it detects their location and suggests the best rated tracks within 30km.
*   **Visual Filters:** Users can filter by what matters to them: *Dog Friendly*, *Wheelchair Access*, *Duration*, and *Difficulty*.

### 🟢 2. Kea Scout AI ("The Guide")
*   **Conversational Search:** Users don't need to tick boxes. They can simply type: *"I'm in Auckland with my elderly parents, looking for a shaded walk with a cafe nearby."*
*   **Intelligent Suggestions:** The AI analyzes the request, searches the database, and checks the weather forecast to provide a tailored recommendation with safety tips.

### 🟢 3. Offline Safety Packs ("The Lifesaver")
*   **One-Click Download:** Because there is no 4G in the forest, users can download a **"Safety PDF"** before they leave.
*   **Content:** Includes a static map, emergency contacts, elevation profile, and the latest official safety alerts.

### 🟢 4. Community & Reviews ("The Pulse")
*   **Real-time Updates:** Hikers can leave "Micro-reviews" (e.g., *"Track is very muddy today"*), providing live conditions that official data often misses.
*   **User Profiles:** Hikers can build a history of tracks they have conquered.

---

## 4. Technical Architecture (Brief Overview)
We are using an enterprise-grade technology stack designed for performance, security, and scalability.

*   **User Interface (Frontend):** Built with **Next.js** (React) & **Tailwind CSS**. This ensures the app works beautifully on mobile phones and loads instantly.
*   **The Brain (Backend):** Built with **Microsoft .NET 10**. This provides robust logic for processing data and handling AI requests.
*   **The Database:** **PostgreSQL**. A powerful database engine capable of performing complex spatial (GPS) calculations.
*   **Data Source:** Synchronized nightly with the **Department of Conservation (DOC) Open API** to ensure official accuracy.
*   **Authentication:** Secure login system powered by **Clerk** (supporting Google Login).

---

## 5. Why KeaTracks?
*   **For Users:** It saves time planning and improves safety in the outdoors.
*   **For the Industry:** It promotes responsible tourism by highlighting safety alerts.
*   **Business Potential:** The "Offline Safety Pack" offers a clear path to monetization via premium subscriptions.