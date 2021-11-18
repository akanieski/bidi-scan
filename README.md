# bidi-scan
A simple command line utility written in C# to identify bidirection unicode attacks.

```
# Just pass it the file or a glob you want to scan

$ bidi-scan src/**.js

Scanning [samples/commenting-out.js]..
Found [RIGHT-TO-LEFT OVERRIDE] in [samples/commenting-out.js] line 4:2
Found [LEFT-TO-RIGHT ISOLATE] in [samples/commenting-out.js] line 4:6
Found [POP DIRECTIONAL ISOLATE] in [samples/commenting-out.js] line 4:19
Found [LEFT-TO-RIGHT ISOLATE] in [samples/commenting-out.js] line 4:21
Found [RIGHT-TO-LEFT OVERRIDE] in [samples/commenting-out.js] line 6:19
Found [LEFT-TO-RIGHT ISOLATE] in [samples/commenting-out.js] line 6:23
Done. Found total of 6 matches in 1 files.

```

```
@article{boucher_trojansource_2021,
    title = {Trojan {Source}: {Invisible} {Vulnerabilities}},
    author = {Nicholas Boucher and Ross Anderson},
    year = {2021},
    journal = {Preprint},
    eprint = {2111.00169},
    archivePrefix = {arXiv},
    primaryClass = {cs.CR},
    url = {https://arxiv.org/abs/2111.00169}
}
```