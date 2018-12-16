import serial
import requests
import time

#Request-related variables
url = "http://hhapi20181020060947.azurewebsites.net/api/values/"
headers = {'Cache-Control':"no-cache",'Postman-Token':"d5bd3194-d61c-4798-83a9-be989721de87"}

#Serial-related variables
ser = serial.Serial('/dev/ttyACM0', 9600)

while 1:
    #Send read-data request to serial port
    ser.write(b'DATA')
    print('Sent DATA request')

    #Wait for serial device to process
    time.sleep(1)

    #Process if response not empty
    if(ser.in_waiting > 0):

        #Read from serial port
        line = ser.readline()

        #Create new string with just number
        lineString = line.decode("utf-8")

        lineString = ''.join(e for e in lineString  if e.isalnum())

        #Send API request
        dataUrl = url  + lineString
        print('dataUrl: ' + dataUrl)
        response = requests.request("GET", dataUrl, headers=headers)
        print(response.text)
