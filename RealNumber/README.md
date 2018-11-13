# Demo of real number parsing from string expression.
##Note: Shows an exmaple of customized parsing logic itself. Real implementation of the task far more simpler :)

**Input**: *string*

**Output**: *bool* value which evaluates to true if string is real number and false if isn't.

```
bool IsRealNumber(string number);
```

### Examples

```
Number.IsRealNumber("0.5"); // true
Number.IRealNumber("-1.66); // true
Number.IsRealNumber("24"); // true
Number.IsRealNumber("-.4"); // true
Number.IsRealNumber("abc"); // false
Number.IsRealNumber("1-2"); // false
Number.IsRealNumber("+-7"); // false
Number.IsRealNumber("..5"); // false
...
```
