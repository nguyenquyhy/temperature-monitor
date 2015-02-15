import RPi.GPIO as GPIO, time, math, multiprocessing, httplib;
import sys;
from w1thermsensor import W1ThermSensor
from sevenSeg import isSet;

def worker(sharedTemp):    
    print('Worker started!');
    
    #tickPeriod = 0.003;
    tickPeriod = 0.008;
    maxTick = 1000;

    flip = True;

    tick = 0;
    digits = [0,0,0,0];
    while True:
        temperature = sharedTemp.value;
        digitIndex = tick % len(digits);

        digits[0] = math.floor(temperature / 10);
        digits[1] = math.floor(temperature) % 10;
        digits[2] = math.floor(temperature * 10) % 10;
        digits[3] = math.floor(temperature * 100) % 10;
    
        GPIO.output(PIN1, flip ^ isSet(1, digits[digitIndex], digitIndex == 1))
        GPIO.output(PIN2, flip ^ isSet(2, digits[digitIndex], digitIndex == 1))
        GPIO.output(PIN3, flip ^ isSet(3, digits[digitIndex], digitIndex == 1))
        GPIO.output(PIN4, flip ^ isSet(4, digits[digitIndex], digitIndex == 1))
        GPIO.output(PIN5, flip ^ isSet(5, digits[digitIndex], digitIndex == 1))
        GPIO.output(PIN7, flip ^ isSet(7, digits[digitIndex], digitIndex == 1))
        GPIO.output(PIN10, flip ^ isSet(10, digits[digitIndex], digitIndex == 1))
        GPIO.output(PIN11, flip ^ isSet(11, digits[digitIndex], digitIndex == 1))
    
        GPIO.output(PIN12, digitIndex == 0)
        GPIO.output(PIN9, digitIndex == 1)
        GPIO.output(PIN8, digitIndex == 2)
        GPIO.output(PIN6, digitIndex == 3)
        
        if tick >= maxTick:
            tick = 0;
    
        tick += 1;
        time.sleep(tickPeriod)

if __name__ == '__main__':
    temperatureRefreshInterval = 2; # in seconds

    GPIO.setmode(GPIO.BCM)
    PIN1 = 17
    PIN2 = 27
    PIN3 = 22
    PIN4 = 5
    PIN5 = 6
    PIN7 = 13
    PIN10 = 19
    PIN11 = 26

    PIN6 = 25 # D4
    PIN8 = 24 # D3
    PIN9 = 23 # D2
    PIN12 = 18 # D1

    GPIO.setup(PIN1, GPIO.OUT)
    GPIO.setup(PIN2, GPIO.OUT)
    GPIO.setup(PIN3, GPIO.OUT)
    GPIO.setup(PIN4, GPIO.OUT)
    GPIO.setup(PIN5, GPIO.OUT)
    GPIO.setup(PIN7, GPIO.OUT)
    GPIO.setup(PIN10, GPIO.OUT)
    GPIO.setup(PIN11, GPIO.OUT)

    GPIO.setup(PIN6, GPIO.OUT)
    GPIO.setup(PIN8, GPIO.OUT)
    GPIO.setup(PIN9, GPIO.OUT)
    GPIO.setup(PIN12, GPIO.OUT)

    sensor = W1ThermSensor()

    sharedTemperature = multiprocessing.Value('d', 0.0);

    p = multiprocessing.Process(target=worker, args=(sharedTemperature,));
    p.daemon = True;
    p.start();

    while (True):
        sharedTemperature.value = sensor.get_temperature();
        print (sensor.id + ': ' + str(sharedTemperature.value));
        try:
            body = '{ "SensorId": "' + sensor.id + '", "Value": ' + str(sharedTemperature.value) + ' }';
            headers = { "Content-type": "application/json" }
            conn = httplib.HTTPConnection(host="temperaturemonitoring.azurewebsites.net")
            conn.request("POST", "/api/temperature", body, headers)
            response = conn.getresponse()
            print response.status, response.reason
            conn.close()
        except:
            e = sys.exc_info()[0];
            print ("Error: " + str(e));

        time.sleep(temperatureRefreshInterval);