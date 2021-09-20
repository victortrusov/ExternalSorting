# ExternalSorting

At the start we have a big file with lines formatted like `{Number}. {String}`

Example:
```
415. Apple
30432. Something something something
1. Apple
32. Cherry is the best
2. Banana is yellow
```

Both parts are not unique. We need to generate another file with same lines but sorted. First we need to sort by `String`, and then by `Number`.

Result example:
```
1. Apple
415. Apple
2. Banana is yellow
32. Cherry is the best
30432. Something something something
```
