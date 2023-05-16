# Mediatr vs No Mediatr benchmarks

This is a follow along to a alternative approach not to use mediator, 
suggested by Anthon from RawCoding youtube channels [link to the video](https://www.youtube.com/watch?v=gIVtrBtR-Yw).

## Bench results

|  Method |     Mean |     Error |    StdDev |   Median |   Gen0 | Allocated |
|-------- |---------:|----------:|----------:|---------:|-------:|----------:|
| Mediatr | 5.452 us | 0.4265 us | 1.2306 us | 4.937 us | 1.5564 |   2.39 KB |
|  Custom | 3.252 us | 0.0600 us | 0.0532 us | 3.234 us | 1.2436 |   1.91 KB |

