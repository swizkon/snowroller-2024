# Congestion Tax Calculator

## Usage:


```
$ curl -X 'POST' \
  'http://localhost:5062/api/congestion-tax/summary' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "vehicle": {
    "vehicleType": "Car"
  },
  "passages": [
    "2024-02-08T08:32:59.985Z"
  ]
}'
```



