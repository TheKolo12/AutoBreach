## AutoBreach 

**AutoBreach** is a plugin for SCP: Secret Laboratory using EXILED . It introduces a dynamic SCP spawn system that automatically **breaches SCPs when their containment doors are opened** , **a generator is activated** or **causing a blackout** — bringing faster and more interactive SCP rounds.

[![downloads](https://img.shields.io/github/downloads/TheKolo12/AutoBreach/total?style=for-the-badge&logo=icloud&color=%233A6D8C)](https://github.com/TheKolo12/AutoBreach/releases/latest)
![Latest](https://img.shields.io/github/v/release/TheKolo12/AutoBreach?style=for-the-badge&label=Latest%20Release&color=%23D91656)
---

### **What It Does**

* When a player opens **SCP-096**, **SCP-173**, or **SCP-049**'s containment door:

  * A random **Spectator** is automatically assigned that SCP role.
  * A configurable **CASSIE announcement** is played.
* When a player activates a **generator**:

  * A random **Spectator** is assigned **SCP-079**.
  * A custom **CASSIE message** plays for immersion.
* When **SCP-079** causes multiple blackouts in:

  * **HCZ-106**, a random **Spectator** becomes **SCP-106** after 3 blackouts.
  * **HCZ-939**, a random **Spectator** becomes **SCP-939** after 3 blackouts.
* Includes **spawn tracking** to prevent breaching the same SCP multiple times.

This plugin creates a more **organic breach system**, especially useful for **event servers**, **chaotic rounds**, or **RP scenarios**.

---

### Requirements

* **EXILED API v9.6.0 or newer**
* SCP\:SL server with plugin support

### Installation

1. Place the compiled `.dll` file into your `EXILED/Plugins` folder.
2. Run the server once to generate the config file.
3. Customize your config as desired.
4. Restart the server — done!

