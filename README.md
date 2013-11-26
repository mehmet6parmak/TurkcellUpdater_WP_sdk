TurkcellUpdater_WP_sdk
======================

Turkcell Updater for Windows Phone is developed to help developers easily handle new versions of their apps. Easily show new version Popups in ForceUpdate, ForceExit and Optional categories or you can show Messages to users by defining them in a remote Turkcell Updater configuration file.

<h1>Turkcell Updater Configuration Guide</h1>
This documents describes usage and structure configuration files used by Turkcell Updater library.
<h2>Usage</h2>

<h3>Updating a Windows Phone application to a new version served on Windows Phone Marketplace</h3>
    { 
        "packageName": "{1fa4c550-7e08-4dae-b3e2-bd2ab24761f3}", 
        "updates": [ 
            { 
                    "descriptions": { 
                        "*": { 
                            "message": "New version available"
                        } 
                    }, 
                    "targetVersionCode": "1.1.0.0"
            } 
        ] 
    }
