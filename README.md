## 🔓 AutoBreach (SCP: Secret Laboratory EXILED Plugin)

**AutoBreach** is a plugin for SCP: Secret Laboratory using the EXILED API. It introduces a dynamic SCP spawn system that automatically **breaches SCPs when their containment doors are opened** or **a generator is activated** — bringing faster and more interactive SCP rounds.

---

### 🧠 What It Does

* When a player opens **SCP-096**, **SCP-173**, or **SCP-049**'s containment door:

  * A random **Spectator** is automatically assigned that SCP role.
  * A configurable **CASSIE announcement** is played.
* When a player activates a **generator**:

  * A random **Spectator** is assigned **SCP-079**.
  * A custom **CASSIE message** plays for immersion.

This plugin creates a more **organic breach system**, especially useful for **event servers**, **chaotic rounds**, or **RP scenarios**.

---

### ⚙️ Features

* 🔁 Automatic SCP assignment from Spectator pool
* 🔊 Configurable CASSIE announcements for each SCP
* 🚪 SCP breach triggered by door interaction (096, 173, 049)
* ⚡ SCP-079 breach triggered by generator activation
* 🧪 Lightweight and optimized for EXILED 9.x

---

### 📦 Requirements

* **EXILED API v9.5.2 or newer**
* SCP\:SL server with plugin support

---

### 🔧 Configuration (example)

```yaml
CassieMessages:
  Scp096:
    message: "SCP 0 9 6 containment breach detected"
  Scp173:
    message: "SCP 1 7 3 breach in progress"
  Scp049:
    message: "SCP 0 4 9 has been released"
  Scp079:
    message: "SCP 0 7 9 reconnected to site network"
```

---

### 🚀 Installation

1. Place the compiled `.dll` file into your `EXILED/Plugins` folder.
2. Run the server once to generate the config file.
3. Customize your config as desired.
4. Restart the server — done!

