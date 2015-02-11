import httplib

def getserial():
  # Extract serial from cpuinfo file
  cpuserial = "0000000000000000"
  try:
    f = open('/proc/cpuinfo','r')
    for line in f:
      if line[0:6] == 'Serial':
        cpuserial = line[10:26]
    f.close()
  except:
    cpuserial = "ERROR000000000"

  return cpuserial

serial = getserial();
body = '{ "deviceId": ' + serial + ', "Value": 50 }'
headers = { "Content-type": "application/json" }
conn = httplib.HTTPConnection(host="temperaturemonitoring.azurewebsites.net")
conn.request("POST", "/api/temperature", body, headers)
response = conn.getresponse()
print response.status, response.reason
conn.close()