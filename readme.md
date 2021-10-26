# Array.Resize vs new

byte 배열 변수 재사용 vs 새로운 배열



프로토콜 개발중에 크기가 다른 버퍼들이 많이 생겨났는데,

이 버퍼를 resize 하여 재활용을 하면 더 효율이 좋지 않을까 싶어 테스트함.



### 실행 장치 사양

```
프로세서	AMD Ryzen 5 3500X 6-Core Processor                3.59 GHz
설치된 RAM	32.0GB
시스템 종류	64비트 운영 체제, x64 기반 프로세서

에디션	Windows 11 Pro
버전	21H2
OS 빌드	22000.282
경험	Windows 기능 경험 팩 1000.22000.282.0
```



### 실험 방식

배열 크기 + 1 N회 반복

바이트 배열 생성

```csharp
byte[] buffer = new byte[1024];
```

Resize 반복 연산

```csharp
var cts = buffer.Length;
for (int i = 0; i < count; i++)
    Array.Resize(ref buffer, cts++);
```

new 반복 연산

```csharp
var cts = buffer.Length;
for (int i = 0; i < count; i++)
    buffer = new byte[cts++];
```

GC 메모리 사용량 측정 (가비지 수집 전)

실행 시간 측정

### 실험 결과

```css
== 10만 회 반복 ==

mod:                    resize
start buffer size:      1024
repetitions:            100000
run time:               464712200ns, 464ms
memory usage:           644664bytes, 629kb

mod:                    new
start buffer size:      1024
repetitions:            100000
run time:               410908400ns, 410ms
memory usage:           644488bytes, 629kb

== 50만 회 반복 ==

mod:                    resize
start buffer size:      1024
repetitions:            500000
run time:               23029301700ns, 23029ms
memory usage:           1043488bytes, 1019kb

mod:                    new
start buffer size:      1024
repetitions:            500000
run time:               15586698300ns, 15586ms
memory usage:           1051568bytes, 1026kb
```

실행 시간에서 new가 조금 더 우세함.

메모리 사용량에서 resize가 조금더 적게 사용했지만, 의미있는 크기는 아님.

가비지 콜렉션 횟수는 두 경우 모두 비슷함.



크기가 다른 버퍼를 resize해서 재활용 할 필요가 없다는것을 알 수 있다.



~~이로서 C#의 가비지 수집기는 수집 성능과 효율이 엄청나다는걸 알 수 있다.~~
