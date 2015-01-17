import httplib;
body = '{ "id": "Test", "Value": 50 }';
headers = { "Content-type": "application/json" }
conn = httplib.HTTPConnection(host="temperaturemonitoring.azurewebsites.net")
conn.request("POST", "/api/temperature", body, headers)
response = conn.getresponse()
print response.status, response.reason
conn.close()