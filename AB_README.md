
### Без балансировки:

```
 ab -n 100000 -c 100 http://localhost/api/v1/statistic
```

```
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


Server Software:        Kestrel
Server Hostname:        localhost
Server Port:            80

Document Path:          /api/v1/statistic
Document Length:        415 bytes

Concurrency Level:      100
Time taken for tests:   69.670 seconds
Complete requests:      100000
Failed requests:        0
Total transferred:      54800000 bytes
HTML transferred:       41500000 bytes
Requests per second:    1435.33 [#/sec] (mean)
Time per request:       69.670 [ms] (mean)
Time per request:       0.697 [ms] (mean, across all concurrent requests)
Transfer rate:          768.13 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   1.1      0      26
Processing:     8   69  20.6     67     249
Waiting:        8   69  20.6     66     249
Total:         10   70  20.6     67     249

Percentage of the requests served within a certain time (ms)
  50%     67
  66%     75
  75%     80
  80%     84
  90%     94
  95%    106
  98%    122
  99%    137
 100%    249 (longest request)
```

_________________________________

```
ab -n 100000 -c 150 http://localhost/api/v1/statistic
```

```
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


Server Software:        Kestrel
Server Hostname:        localhost
Server Port:            80

Document Path:          /api/v1/statistic
Document Length:        415 bytes

Concurrency Level:      150
Time taken for tests:   71.224 seconds
Complete requests:      100000
Failed requests:        0
Total transferred:      54800000 bytes
HTML transferred:       41500000 bytes
Requests per second:    1404.03 [#/sec] (mean)
Time per request:       106.836 [ms] (mean)
Time per request:       0.712 [ms] (mean, across all concurrent requests)
Transfer rate:          751.37 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   1.9      0      31
Processing:    21  106  32.2    101     327
Waiting:       21  105  32.2    100     327
Total:         25  107  32.1    101     328

Percentage of the requests served within a certain time (ms)
  50%    101
  66%    115
  75%    124
  80%    131
  90%    149
  95%    165
  98%    188
  99%    203
 100%    328 (longest request)
```


### С балансировки:

```
ab -n 100000 -c 100 http://localhost/api/v1/statistic
```


```
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


Server Software:        Kestrel
Server Hostname:        localhost
Server Port:            80

Document Path:          /api/v1/statistic
Document Length:        415 bytes

Concurrency Level:      100
Time taken for tests:   62.982 seconds
Complete requests:      100000
Failed requests:        0
Total transferred:      54800000 bytes
HTML transferred:       41500000 bytes
Requests per second:    1587.76 [#/sec] (mean)
Time per request:       62.982 [ms] (mean)
Time per request:       0.630 [ms] (mean, across all concurrent requests)
Transfer rate:          849.70 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   2.8      0      57
Processing:     2   62  67.4     54    2160
Waiting:        0   61  67.4     53    2160
Total:          2   63  67.3     55    2162

Percentage of the requests served within a certain time (ms)
  50%     55
  66%     64
  75%     73
  80%     80
  90%     96
  95%    110
  98%    128
  99%    144
 100%   2162 (longest request)
```

```
ab -n 100000 -c 150 http://localhost/api/v1/statistic
```

```
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


Server Software:        Kestrel
Server Hostname:        localhost
Server Port:            80

Document Path:          /api/v1/statistic
Document Length:        415 bytes

Concurrency Level:      150
Time taken for tests:   53.842 seconds
Complete requests:      100000
Failed requests:        0
Total transferred:      54800000 bytes
HTML transferred:       41500000 bytes
Requests per second:    1857.29 [#/sec] (mean)
Time per request:       80.763 [ms] (mean)
Time per request:       0.538 [ms] (mean, across all concurrent requests)
Transfer rate:          993.94 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   1.8      0      29
Processing:     4   80  27.3     78     230
Waiting:        4   79  27.2     78     230
Total:          4   81  27.2     79     230

Percentage of the requests served within a certain time (ms)
  50%     79
  66%     88
  75%     94
  80%     99
  90%    115
  95%    130
  98%    148
  99%    160
 100%    230 (longest request)
```

### Вывод:

При увеличении кол-ва конкурирующих запросов ухудшаются результаты.

При балансировке улучшаются результаты.