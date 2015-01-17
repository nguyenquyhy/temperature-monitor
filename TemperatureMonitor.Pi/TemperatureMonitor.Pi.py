from w1thermsensor import W1ThermSensor
import RPi.GPIO as GPIO, time, math;
from sevenSeg import isSet;
import multiprocessing;

def worker(sharedTemperature):    
    temperature = sharedTemperature.value;
    tickPeriod = 0.005;
    maxTick = 1000;
    tick = 0;
    digits = [0,0,0,0];
    while True:
        digitIndex = tick % len(digits);

        digits[0] = math.floor(temperature / 10);
        digits[1] = math.floor(temperature) % 10;
        digits[2] = math.floor(temperature * 10) % 10;
        digits[3] = math.floor(temperature * 100) % 10;
        #print str(digit0) + ' ' + str(digit1);
    
        GPIO.output(PIN1, isSet(1, digits[digitIndex], digitIndex == 1))
        GPIO.output(PIN2, isSet(2, digits[digitIndex], digitIndex == 1))
        GPIO.output(PIN3, isSet(3, digits[digitIndex], digitIndex == 1))
        GPIO.output(PIN4, isSet(4, digits[digitIndex], digitIndex == 1))
        GPIO.output(PIN5, isSet(5, digits[digitIndex], digitIndex == 1))
        GPIO.output(PIN7, isSet(7, digits[digitIndex], digitIndex == 1))
        GPIO.output(PIN10, isSet(10, digits[digitIndex], digitIndex == 1))
        GPIO.output(PIN11, isSet(11, digits[digitIndex], digitIndex == 1))
    
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

    p = multiprocessing.Process(target=worker, args=(sharedTemperature));
    p.daemon = True;
    p.start();

    while (True):
        sharedTemperature.value = sensor.get_temperature();
        print (sensor.id + ': ' + str(sharedTemperature.value));
        time.sleep(temperatureRefreshInterval);