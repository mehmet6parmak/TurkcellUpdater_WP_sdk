TurkcellUpdater_WP_sdk
======================

Turkcell Updater for Windows Phone is developed to help developers easily handle new versions of their apps. Easily show new version Popups in ForceUpdate, ForceExit and Optional categories or you can show Messages to users by defining them in a remote Turkcell Updater configuration file.

<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>Turkcell Updater Configuration Guide</title>
    <style type="text/css">
        table, th, td
        {
            border: 1px solid black;
        }

        th, td
        {
            padding: 8px;
        }

        table
        {
            border-collapse: collapse;
            border-color: black;
        }
    </style>
</head>
<body>
    <h1>Turkcell Updater Configuration Guide</h1>
    This documents describes usage and structure configuration files used by Turkcell Updater library.
    <h2>Usage<br />
    </h2>

    <h3>Updating a Windows Phone application to a new version served on Windows Phone Marketplace</h3>
    <pre>
        <code>
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
        </code>
    
</pre>

    <h3>Forcing a Windows Phone application to quit.</h3>
    <pre>
        <code>
{ 
    "packageName": "{1fa4c550-7e08-4dae-b3e2-bd2ab24761f3}", 
    "updates": [ 
        { 
             "descriptions": { 
                 "*": { 
                     "message": "Service wont be available anymore!"
                 } 
             }, 
             "forceExit": true
        } 
    ] 
} 
        </code>
    
</pre>
    <h3>End of support for older WP versions.</h3>
    <pre>
        <code>
{ 
    "packageName": "{1fa4c550-7e08-4dae-b3e2-bd2ab24761f3}", 
    "updates": [ 
        { 
            "filters": { 
                "deviceOsVersion": "9.*"
            }, 
            "descriptions": { 
                 "*": { 
                     "message": "New version available"
                 } 
            }, 
            "targetWebsiteUrl": "<a href="http://www.windowsphone.com/tr-tr/store/app/turkcell-online-İşlemler/671f8ed8-7072-4980-9cc4-6da646f0d9fd">http://www.windowsphone.com/tr-tr/store/app/turkcell-online-%C4%B0%C5%9Flemler/671f8ed8-7072-4980-9cc4-6da646f0d9fd</a>", 
            "forceUpdate": true
        }, 
        { 
            "filters": { 
                "deviceOsVersion": "8.*"
            }, 
  
             "descriptions": { 
                 "*": { 
                     "message": "WP versions earlier than 9.0 are not supported."
                 } 
             }, 
             "forceExit": true
        } 
    ] 
}

        </code>  </pre>

    <h3>Transferring users to another WP application replacing the old one.</h3>
    <pre>
        <code>
{
	"packageName": "{1fa4c550-7e08-4dae-b3e2-bd2ab24761f3}",
	"updates": [
		{
			"descriptions": {
				 "*": {
					 "message": "New version available"
				 }
			},
			"targetPackageName": "{64b25472-6635-4a78-bee5-84374d437134}",
			"targetVersionCode": "2.3",
			"forceUpdate": true
		}
	]
}
</code></pre>


    <h3>WP multilingual update messages.</h3>
    <pre>
        <code>
{ 
    "packageName": "{1fa4c550-7e08-4dae-b3e2-bd2ab24761f3}", 
    "updates": [ 
        { 
            "filters": { 
                "appVersionCode": "<2.0", 
                "deviceOsName": "wp"
            }, 
            "descriptions": { 
                "tr": { 
                    "message": "Uygulamanın yeni sürümü yayınlandı.", 
                    "whatIsNew": "Hata düzeltildi", 
                    "warnings": "Yeni sürüm ek yetkiler gerektirir"
                }, 
                "*": { 
                    "message": "New version available", 
                    "whatIsNew": "Minor bug fixes", 
                    "warnings": "New version requires additional privileges"
                } 
            }, 
            "targetVersionCode": 10 
        } 
    ] 
}
        </code>
    </pre>

    <h2>Reference</h2>
    Different configuration files are stored per application using Turkcell Updater Library.<br>
    Configuration files are UTF-8 encoded JSON documents and should be served with <code>"application/json"</code> content type.
Since configurations may contain vulnerable information like URL of update package they should be only accessible via HTTPS.<br>
    Root element of files should conform to <a href="#configurationRoot">Configuration Root</a> specifications below.<br />
    Documents may contain additional keys but Updater library ignores any other key that is not referred in this document.


    <!-- configurationRoot -->
    <div id="configurationRoot" class="entry">
        <h3>Configuration Root</h3>
        Root object that contains all data needed by Updater Library to show update messages and other notifications to user.
        <h4>Structure:</h4>
        Type: Object

        <table>
            <tr>
                <th>Property name</th>
                <th>Type</th>
                <th>Default value</th>
                <th>Platforms</th>
                <th>Description</th>
                <th>Required</th>
                <th>Since</th>
            </tr>

            <tr>
                <td>packageName</td>
                <td>String</td>
                <td>null</td>
                <td>All</td>
                <td>Platform specific unique identifier of application which configuration is created for. Package Id for Windows Phone applications.</td>
                <td>Yes</td>
                <td>1</td>
            </tr>

            <tr>
                <td>updates</td>
                <td>Array</td>
                <td>null</td>
                <td>All</td>
                <td>List of update entries with 0 or more elements. See <a href="#updateEntry">Update Entry</a></td>
                <td>No</td>
                <td>1</td>
            </tr>

            <tr>
                <td>messages</td>
                <td>Array</td>
                <td>null</td>
                <td>All</td>
                <td>List of messages with 0 or more elements. See <a href="#messageEntry">Message Entry</a></td>
                <td>No</td>
                <td>2</td>
            </tr>

        </table>

        <h4>Example:</h4>
        <h3>Windows Phone</h3>
        <pre>
            <code>
{
	"packageName": "{1fa4c550-7e08-4dae-b3e2-bd2ab24761f3}",
	"updates": [
		{
			 "descriptions": {
				 "*": {
					 "message": "New version available",
					 "whatIsNew": "Minor bug fixes",
					 "warnings": "New version requires additional privileges"
				 }
			 },
			 "targetWebsiteUrl": "http://www.windowsphone.com/tr-tr/store/app/turkcell-online-%C4%B0%C5%9Flemler/671f8ed8-7072-4980-9cc4-6da646f0d9fd",
			 "forceUpdate": false
		}
	],
	"messages": [
		{
			"descriptions": {
				"*": {
					"title": "Offer",
					"message": "New application is available!",
					"imageUrl": "http://example.com/app2-icon.png"
				}
			},
			"targetWebsiteUrl": "http://www.windowsphone.com/tr-tr/store/app/turkcell-online-%C4%B0%C5%9Flemler/671f8ed8-7072-4980-9cc4-6da646f0d9fd",
			"maxDisplayCount": 3
		}
	]
}
            </code>        </pre>

    </div>

    <!-- updateEntry -->
    <div id="updateEntry">
        <h3>Update Entry</h3>
        Provides information about how update should be installed and when update should be applied.
        <h4>Structure:</h4>
        Type: Object

        <table>
            <tr>
                <th>Property name</th>
                <th>Type</th>
                <th>Default value</th>
                <th>Platforms</th>
                <th>Description</th>
                <th>Required</th>
                <th>Since</th>
            </tr>

            <tr>
                <td>filters</td>
                <td>Array</td>
                <td>null</td>
                <td>All</td>
                <td>See <a href="#filterEntry">Filter Entry</a></td>
                <td>No</td>
                <td>1</td>
            </tr>

            <tr>
                <td>descriptions</td>
                <td>Object</td>
                <td>null</td>
                <td>All</td>
                <td>Map of update description entries. Keys (property names) are two letter language codes (see: <a href="http://en.wikipedia.org/wiki/List_of_ISO_639-1_codes">ISO 639-1</a>) and values are <a href="#updateDescriptionEntry">Update Description Entries</a>.
If empty strings (&quot;&quot;) or asterisk(&quot;*&quot;) is used as key, it matches with any language.<br>
                    <br>
                    For iOS : If device language is English but the application language is Turkish asterisk(&quot;*&quot;)language code is suggested for displaying Turkish descriptions.</td>
                <td>Yes</td>
                <td>1</td>
            </tr>

            <tr>
                <td>targetVersionCode</td>
                <td>Number</td>
                <td>-1</td>
                <td>WP</td>
                <td>Target version number. See <a href="#updateEntryNote1">Note #1</a><br />
                    </td>
                <td>Yes for WP application, unless <code>forceExit</code> is <code>true</code></td>
                <td>1</td>
            </tr>
            
            <tr>
                <td>targetPackageName</td>
                <td>String</td>
                <td>Current application&#39;s package id.</td>
                <td>WP</td>
                <td>Package Id of new version. See <a href="#updateEntryNote2">Note #2</a></td>
                <td>No</td>
                <td>1</td>
            </tr>
            
            <tr>
                <td>forceUpdate</td>
                <td>Boolean</td>
                <td>false</td>
                <td>All</td>
                <td><code>true</code> if user should not skip this update and continue to use application. When <code>true</code> "Exit application" option will be displayed to user instead of "Remind me later" option.</td>
                <td>No</td>
                <td>1</td>
            </tr>

            <tr>
                <td>forceExit</td>
                <td>Boolean</td>
                <td>false</td>
                <td>All</td>
                <td><code>true</code> if user should not have any option other than exiting application. When <code>true</code> only "Exit application" option will be displayed to user.</td>
                <td>No</td>
                <td>2</td>
            </tr>

            <tr>
                <td>targetWebsiteUrl</td>
                <td>String</td>
                <td>null</td>
                <td>All</td>
                <td>Windows Phone Marketplace link of the target application.</td>
                <td>See <a href="#updateEntryNote2">Note #2</a></td>
                <td>1</td>
            </tr>

            <tr>
                <td>targetUriSchema</td>
                <td>String</td>
                <td>null</td>
                <td>WP</td>
                <td>Uri schema to launch target application if it is already installed. Please note that Updater sdk can only check installations from the same publisher. If the target apps&#39; and current apps&#39; publishers are different then probably Store application will be launched to show the details page of the target application.</td>
                <td>See <a href="#updateEntryNote2">Note #2</a></td>
                <td>1</td>
            </tr>
        </table>

        <h4>Notes:</h4>
        <ol>
            <li id="updateEntryNote1">Version entries will be omitted if <code>targetPackageName</code> is same with current applications package name and <code>targetVersionCode</code> is same with current applications version code. This check is performed in order avoid updates to existing version.</li>
            <li id="updateEntryNote2">Any update entry should meet at least one of the following conditions, Otherwise current applications details page will be opened in Store application.
            <ul>
                <li><code>forceExit</code> is <code>true</code></li>
                <li><code>targetPackageName</code> is not <code>null</code> or empty</li>
                <li><code>targetWebsiteUrl</code> is not <code>null</code> or empty</li>
                <li><code>targetUriSchema</code> is not <code>null</code> or empty</li>
            </ul>


            </li>


        </ol>



        <h4>Example:</h4>


        <h3>Windows Phone, launch target app if it is already installed. </h3>
        <pre>
            <code>
{
	"packageName": "{1fa4c550-7e08-4dae-b3e2-bd2ab24761f3}",
	"updates": [
		{
			"descriptions": {
				 "*": {
					 "message": "New version available"
				 }
			},
			"targetPackageName": "{50282631-ff07-49f8-a313-de06022ca461}",
			"targetVersionCode": "1.0",
			"targetUriSchema": "updatersample://helloworld",
			"forceUpdate": true
		}
	]
}

            </code>
        </pre>

    </div>



    <!-- messageEntry -->
    <div id="messageEntry" class="entry">
        <h3>Message Entry</h3>
        Defines a message to display to user when message should be displayed.
        <h4>Structure:</h4>
        Type: Object

        <table>
            <tr>
                <th>Property name</th>
                <th>Type</th>
                <th>Default value</th>
                <th>Platforms</th>
                <th>Description</th>
                <th>Required</th>
                <th>Since</th>
            </tr>

            <tr>
                <td>filters</td>
                <td>Array</td>
                <td>null</td>
                <td>All</td>
                <td>See <a href="#filterEntry">Filter Entry</a></td>
                <td>No</td>
                <td>2</td>
            </tr>

            <tr>
                <td>descriptions</td>
                <td>Object</td>
                <td>null</td>
                <td>All</td>
                <td>Map of message description entries. Keys (property names) are two letter language codes (see: <a href="http://en.wikipedia.org/wiki/List_of_ISO_639-1_codes">ISO 639-1</a>) and values are <a href="#messageDescriptionEntry">Message Description Entries</a>.
If empty strings (&quot;&quot;) or asterisk(&quot;*&quot;) is used as key, it matches with any language.</td>
                <td>Yes</td>
                <td>2</td>
            </tr>

            <tr>
                <td>id</td>
                <td>Number</td>
                <td>For WP, Auto generated value using <code>targetWebsiteUrl</code>, <code>targetPackageName</code> and <code>descriptions</code>.</td>
                <td>All</td>
                <td>Unique ID of message. ID is used when determining last display date and total display count of message.</td>
                <td>No</td>
                <td>2</td>
            </tr>

            <tr>
                <td>targetPackageName</td>
                <td>String</td>
                <td>null</td>
                <td>WP</td>
                <td>WP: Target applications&#39; (offered application) package id.&nbsp; </td>
                <td>No</td>
                <td>2</td>
            </tr>

            <tr>
                <td>targetWebsiteUrl</td>
                <td>String</td>
                <td>null</td>
                <td>All</td>
                <td>URL of web page that contains offered application.</td>
                <td>No</td>
                <td>2</td>
            </tr>

            <tr>
                <td>maxDisplayCount</td>
                <td>Number</td>
                <td>2147483647</td>
                <td>All</td>
                <td>Maximum display count of message</td>
                <td>No</td>
                <td>2</td>
            </tr>

            <tr>
                <td>displayBeforeDate</td>
                <td>String</td>
                <td>null</td>
                <td>All</td>
                <td>If not null, message should not be displayed after this date. For date format details see <a href="#messageEntryNote1">Note #1</a></td>
                <td>No</td>
                <td>2</td>
            </tr>

            <tr>
                <td>displayAfterDate</td>
                <td>String</td>
                <td>null</td>
                <td>All</td>
                <td>If not null, message should not be displayed before this date. For date format details see <a href="#messageEntryNote1">Note #1</a></td>
                <td>No</td>
                <td>2</td>
            </tr>

            <tr>
                <td>displayPeriodInHours</td>
                <td>Number</td>
                <td>0</td>
                <td>All</td>
                <td>Minimum duration in hours that should pass before displaying this message again</td>
                <td>No</td>
                <td>2</td>
            </tr>

        </table>

        <h4>Notes:</h4>
        <ol>
            <li id="messageEntryNote1">Following date formats from <a href="http://en.wikipedia.org/wiki/ISO_8601">ISO 8601</a> are supported:
	<ul>
        <li>&quot;yyyy-MM-dd&quot; example: &quot;1969-12-31&quot; &quot;1970-01-01&quot;</li>
        <li>&quot;yyyy-MM-dd HH:mm&quot; example: &quot;1969-12-31 16:00&quot;, &quot;1970-01-01 00:00&quot;</li>
    </ul>
        </ol>

        <h4>Example:</h4>
        <h4>Windows Phone</h4>
        <pre>
            <code>
{ 
    "packageName": "{1fa4c550-7e08-4dae-b3e2-bd2ab24761f3}", 
    "messages": [ 
        { 
            "descriptions": { 
                "*": { 
                    "title": "Offer", 
                    "message": "New application is available!", 
                    "imageUrl": "http://technology.inquirer.net/files/2013/10/Microsoft-Windows-8.1.jpg"
                } 
            }, 
            "targetPackageName": "{64b25472-6635-4a78-bee5-84374d437134}", 
            "targetGooglePlay": true, 
            "displayAfterDate": "2013-01-01", 
            "displayBeforeDate": "2013-12-01", 
            "maxDisplayCount": 20 
        } 
    ] 
} 
            </code>

        </pre>

    </div>

    <!-- filterEntry -->
    <div id="filterEntry" class="entry">
        <h3>Filter Entry</h3>
        <a href="#messageEntry">Message Entries</a> and <a href="#updateEntry">Update Entries</a> are applied only
if filter matches with device properties. If "filter" property of a <a href="#messageEntry">Message Entry</a> and a <a href="#updateEntry">Update Entry</a>
        is omitted or defined as null no filtering will be applied.<br>
        Filter entries consist of key and value pairs.
Keys are property names and values are filtering rules.
        <br>
        Filtering rules format applies to all values of filter entry:<br>
        <ul>
            <li>Rules are sequences of rule parts joined with ","</li>
            <li>Both rule parts and values are converted to lower case and trimmed before
comparison</li>
            <li>Order of rule parts doesn't change the result, example: "!b,a" is same with "a,!b"</li>
            <li><code>"*"</code>, <code>null</code> or empty string matches with any value including
                <code>null</code></li>
            <li><code>"''"</code> matches with <code>null</code> or empty string</li>
            <li><code>"!''"</code> matches with any value except <code>null</code> or empty string</li>
            <li><code>"![rule part]"</code> excludes any value matches with [rule]</li>
            <li><code>"[value]"</code> matches with any value equals to [value]</li>
            <li><code>"[prefix]*"</code> matches with any value starting with [prefix]</li>
            <li><code>"*[suffix]"</code> matches with any value ending with [suffix]</li>
            <li><code>"[prefix]*[suffix]"</code> matches with any value starting with [prefix] and
ending with [suffix]</li>
            <li><code>"&gt;[integer]"</code> matches with any value greater than [integer]</li>
            <li><code>"&gt;=[integer]"</code> matches with any value greater than or equals to [integer]</li>
            <li><code>"&lt;[integer]"</code> matches with any value lesser than [integer]</li>
            <li><code>"&lt;=[integer]"</code> matches with any value lesser than or equals to [integer]</li>
            <li><code>"&lt;&gt;[integer]"</code> matches with any value not equals to [integer]</li>
        </ul>

        <h4>Structure:</h4>
        Type: Object

        <table>
            <tr>
                <th>Property name</th>
                <th>Type</th>
                <th>Default value</th>
                <th>Platforms</th>
                <th>Description</th>
                <th>Required</th>
                <th>Since</th>
            </tr>

            <tr>
                <td>appPackageName</td>
                <td>String</td>
                <td>null</td>
                <td>All</td>
                <td>Filter rule for package name of application.<br>
                    Example value: "com.sample.app".
                </td>
                <td>No</td>
                <td>1</td>
            </tr>
         
            <tr>
                <td>appVersionCode</td>
                <td>String</td>
                <td>null</td>
                <td>WP</td>
                <td>Application version. Major and Minor numbers are mandatory.<br />
                    Example: 1.1.0.0 or 2.3<br />
                </td>
                <td>No</td>
                <td>1</td>
            </tr>

            <tr>
                <td>deviceOsName</td>
                <td>String</td>
                <td>null</td>
                <td>WP</td>
                <td>Name of operating system of device.<br>
                    Values: "android", "ios", "wp".
                </td>
                <td>No</td>
                <td>1</td>
            </tr>

            <tr>
                <td>deviceOsVersion</td>
                <td>String</td>
                <td>null</td>
                <td>All</td>
                <td>Version name of operating system of device.<br>
                    Example value: "2.3.3".
                </td>
                <td>No</td>
                <td>1</td>
            </tr>

            <tr>
                <td>deviceBrand</td>
                <td>String</td>
                <td>null</td>
                <td>WP</td>
                <td>Brand name of device.<br>
                    Example value: &quot;NOKIA&quot;
                </td>
                <td>No</td>
                <td>1</td>
            </tr>

            <tr>
                <td>deviceModel</td>
                <td>String</td>
                <td>null</td>
                <td>All</td>
                <td>Model name of device.
                </td>
                <td>No</td>
                <td>1</td>
            </tr>

            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td>deviceIsTablet</td>
                <td>String</td>
                <td>null</td>
                <td>All</td>
                <td>For Windows Phone this value is <code>false</code></td>
                <td>No</td>
                <td>1</td>
            </tr>

            <tr>
                <td>deviceLanguage</td>
                <td>String</td>
                <td>null</td>
                <td>All</td>
                <td>Two letter language code of device
(see: <a href="http://en.wikipedia.org/wiki/List_of_ISO_639-1_codes">ISO 639-1</a>).<br>
                    Example values: "en", "tr", "fr".
                </td>
                <td>No</td>
                <td>1</td>
            </tr>

            <tr>
                <td>x-&lt;Application defined key&gt;</td>
                <td>String</td>
                <td>null</td>
                <td>WP</td>
                <td>"x-" is prefix for application defined keys of arbitrary properties.<br>
                    Applications may define and add own custom property key-value pairs for application specific filters.
                    <br>
                    Example values: "x-foo", "x-bar".
                </td>
                <td>No</td>
                <td>1</td>
            </tr>

            <tr>
                <td>deviceMcc</td>
                <td>String</td>
                <td>null</td>
                <td>WP</td>
                <td>For Windows Phone, only operators in Turkey are available.<br />
                    See <a href="http://en.wikipedia.org/wiki/Mobile_country_code">Mobile country code</a><br>
                    Example value: "286" for Turkey.</td>
                <td>No</td>
                <td>1</td>
            </tr>

            <tr>
                <td>deviceMnc</td>
                <td>String</td>
                <td>null</td>
                <td>WP</td>
                <td>For Windows Phone, only operators in Turkey are available.<br />
                    Mobile network code of device. See <a href="http://en.wikipedia.org/wiki/Mobile_country_code">Mobile country code</a><br>
                    Example value: "01" for Turkcell. </td>
                <td>No</td>
                <td>1</td>
            </tr>

            <tr>
                <td>updaterLevel</td>
                <td>String</td>
                <td>null</td>
                <td>WP</td>
                <td>An integer number that is used to define updater version used by application.<br>
                    Example value: "1" for initial version of updater sdk.
                </td>
                <td>No</td>
                <td>1</td>
            </tr>

        </table>

        <h4>Example:</h4>
        <pre>
<code>
{
	"deviceOsName": "wp",
	"deviceOsVersion": "8.*",
	"appVersionCode": "2.4.3, 2.4.4, 2.5.*, 3.*, 4.*, 5.*",
	"deviceIsTablet": &quot;false"
}
</code>
</pre>

    </div>

    <!-- updateDescriptionEntry -->
    <div id="updateDescriptionEntry" class="entry">
        <h3>Update Description Entry</h3>
        Contains language specific texts that are displayed to user on updates found dialog.
        <br>
        See <a href="#updateEntry">Update Entry</a>
        <br>

        <h4>Structure:</h4>
        Type: Object

        <table>
            <tr>
                <th>Property name</th>
                <th>Type</th>
                <th>Default value</th>
                <th>Platforms</th>
                <th>Description</th>
                <th>Required</th>
                <th>Since</th>
            </tr>

            <tr>
                <td>message</td>
                <td>String</td>
                <td>null</td>
                <td>All</td>
                <td>Summary information describing update contents.
                </td>
                <td>Yes</td>
                <td>1</td>
            </tr>

            <tr>
                <td>whatIsNew</td>
                <td>String</td>
                <td>null</td>
                <td>All</td>
                <td>Describes changes and new features of new version.
                </td>
                <td>No</td>
                <td>1</td>
            </tr>

            <tr>
                <td>warnings</td>
                <td>String</td>
                <td>null</td>
                <td>All</td>
                <td>Warning text about the update. Any important issues that user should know before updating should be described here.
                </td>
                <td>No</td>
                <td>1</td>
            </tr>

        </table>

        <h4>Example:</h4>
        <pre>
<code>
{
	"message": "New version available",
	"whatIsNew": "Minor bug fixes",
	"warnings": "New version requires additional privileges"
}
</code>
</pre>

    </div>

    <!-- messageDescriptionEntry -->
    <div id="messageDescriptionEntry" class="entry">
        <h3>Message Description Entry</h3>
        Contains language specific texts that are displayed to user on message dialog.
        <br>
        See <a href="#messageEntry">Message Entry</a>
        <br>

        <h4>Structure:</h4>
        Type: Object

        <table>
            <tr>
                <th>Property name</th>
                <th>Type</th>
                <th>Default value</th>
                <th>Platforms</th>
                <th>Description</th>
                <th>Required</th>
                <th>Since</th>
            </tr>

            <tr>
                <td>title</td>
                <td>String</td>
                <td>null</td>
                <td>All</td>
                <td>Text that will be displayed at title of message dialog.
                </td>
                <td>No</td>
                <td>2</td>
            </tr>

            <tr>
                <td>message</td>
                <td>String</td>
                <td>null</td>
                <td>All</td>
                <td>Text displayed inside message dialog.
                </td>
                <td>Yes</td>
                <td>2</td>
            </tr>

            <tr>
                <td>imageUrl</td>
                <td>String</td>
                <td>null</td>
                <td>All</td>
                <td>Fully qualified URL of image file that is displayed in message dialog. It should refer to a square PNG or JPEG with preferably at 100x100 pixels size.
                </td>
                <td>No</td>
                <td>2</td>
            </tr>

        </table>

        <h4>Example:</h4>
        <pre>
<code>
{
	"title": "Offer",
	"message": "New application is avaliable!",
	"imageUrl": "http://example.com/app2-icon.png"
}
</code>
</pre>

    </div>


</body>
</html>
