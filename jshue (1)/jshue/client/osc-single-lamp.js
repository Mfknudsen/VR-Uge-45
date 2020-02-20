/*
Lad os prøve at lave et program som modtager to variable til at styre lysstyrke og temperatur med osc variablene lys og varme

BEMÆRK for at et javascript kan modtage OSC skal det køre filen bridge.js med node. Find din terminal og find mappen som projektet ligger i. Her ligger der en fil ved navn bridge.js 

Skriv denne kommando i terminalen:

node bridge.js
*/
var switchOSC = false;

// Bridge ip-adresse. Find den fx i hue app'en
var url = '192.168.0.102';
// Fælles brugernavn
var username = 'rLGZCRauv9Beqxx60ItdtkkoG5-20G4r4PbqmR7P';
//Slidere
var dimmer, temper;
//Den pære du vil kontrollere
var lightNumber = 19;
//Den osc besked du vil modtage
var osc_address = "/wek/outputs";

function setup() {
    createCanvas(500, 500);
    setupOsc(12000, 6448); //Begynd at lytte efter OSC

    oscDiv = createDiv('OSC response'); // a div for the Hue hub's responses
    oscDiv.position(10, 140); // position it

    resultDiv = createDiv('Hub response'); // a div for the Hue hub's responses
    resultDiv.position(10, 200); // position it

    textSize(144);
    text(lightNumber, 300, 100);
    connect(); // connect to Hue hub; it will show all light states
}

/*
this function makes the HTTP GET call to get the light data:
HTTP GET http://your.hue.hub.address/api/username/lights/
*/
function connect() {
    url = "http://" + url + '/api/' + username + '/lights/';
    httpDo(url, 'GET', getLights);
}

/*
this function uses the response from the hub
to create a new div for the UI elements
*/
function getLights(result) {
    resultDiv.html("<hr/>" + result);
}

function oscChangeOnOff() {

    if (switchOSC) {
        switchOSC = false;
    } else if (!switchOSC) {
        switchOSC = true;
    }

    var lightState = {
        on: switchOSC,
    }
    setLight(lightNumber, lightState);

}

/*
this function makes an HTTP PUT call to change the properties of the lights:
HTTP PUT http://your.hue.hub.address/api/username/lights/lightNumber/state/
and the body has the light state:
{
  on: true/false,
  bri: brightness
}
*/
function setLight(whichLight, data) {
    var path = url + whichLight + '/state/';

    var content = JSON.stringify(data); // convert JSON obj to string
    httpDo(path, 'PUT', content, 'text', getLights); //HTTP PUT the change
}


/*
Nedenstående er OSC funktioner. 
*/

function sendOsc(address, value) {
    socket.emit('message', [address].concat(value));
}

function setupOsc(oscPortIn, oscPortOut) {
    var socket = io.connect('http://127.0.0.1:8081', {
        port: 8081,
        rememberTransport: false
    });
    socket.on('connect', function () {
        socket.emit('config', {
            server: {
                port: oscPortIn,
                host: '127.0.0.1'
            },
            client: {
                port: oscPortOut,
                host: '127.0.0.1'
            }
        });
    });
    socket.on("message", function (msg) {
            if (msg[1][0] == 0) {
                switchOSC = true;
                oscChangeOnOff(switchOSC);
            } else if (msg[1][0] == 1) {
                switchOSC = false;
                oscChangeOnOff(switchOSC);
            }
    });
}







//Pseudokode til fremtidige projekter



/*
Gøre colorloop afhængig af en afbildning af hånden, taget med leap motion

Jeg skal altså bruge leapmotion sketchen fra kadenze som input og denne sketch skal så være output sketchen

Det vil kræve at jeg sætter send OSC message til at opsnappe beskeden fra wekinator og derfra sende et output til 
port fra port 12000

Det kræver også at jeg bruger nogle kodestumper fra tidligere processing projekter og retter disse til

Jeg skal hole overblik over de nye funktioner jeg indsætter og jeg skal have fundet, der hvor der i denne kode bliver
sendt en OSC besked til port 12000 så jeg kan ændre denne til at være afhængig af inputtet også

*/