# What is this?
A simple C# class that converts a list of string paths into a tree model.

# How does that look?
Given an input like:

```
"//subFolder1",
"//subFolder1//subsubfolder1a",
"//subFolder1//subsubfolder1a//sub-sub-sub",
"//subFolder2",
"//subFolder2//subsubfolder1b"
```

turns into: 

```
 + subFolder1
     + subsubfolder1a
         + sub-sub-sub
 + subFolder2
     + subsubfolder1b
```

# How does that work?
Easy...
```
var paths = new[]
{
    "//subFolder1",
    "//subFolder1//subsubfolder1a",
    "//subFolder1//subsubfolder1a//sub-sub-sub",
    "//subFolder2",
    "//subFolder2//subsubfolder1b"
};

var converter = new PathsToTreeConverter();
converter.SetDelimiterSymbol("//"); // "/" is default, otherwise use the SetDelimiterSymbol method

var result = converter.Convert(paths);

// now do something funky with your list of tree nodes
```