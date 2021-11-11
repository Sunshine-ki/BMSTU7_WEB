 ab -n 100000 -c 100 http://localhost/api/v1/statistic
This is ApacheBench, Version 2.3 <$Revision: 1807734 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 10000 requests
Completed 20000 requests
Completed 30000 requests
Completed 40000 requests
Completed 50000 requests
Completed 60000 requests
Completed 70000 requests
Completed 80000 requests
Completed 90000 requests
Completed 100000 requests
Finished 100000 requests


Server Software:        nginx/1.14.0
Server Hostname:        localhost
Server Port:            80

Document Path:          /api/v1/statistic
Document Length:        415 bytes

Concurrency Level:      100
Time taken for tests:   65.076 seconds
Complete requests:      100000
Failed requests:        0
Total transferred:      56200000 bytes
HTML transferred:       41500000 bytes
Requests per second:    1536.66 [#/sec] (mean)
Time per request:       65.076 [ms] (mean)
Time per request:       0.651 [ms] (mean, across all concurrent requests)
Transfer rate:          843.36 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.9      0      28
Processing:     8   65  30.1     61     842
Waiting:        7   64  29.9     61     842
Total:         10   65  30.0     62     842

Percentage of the requests served within a certain time (ms)
  50%     62
  66%     69
  75%     74
  80%     77
  90%     86
  95%     95
  98%    110
  99%    125
 100%    842 (longest request)
