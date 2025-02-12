# kertaus1

## 1) Make a console application that:

    Accepts one command line argument, which must be an integer.
        <https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/main-command-line>

        https://stackoverflow.com/questions/44326321/is-its-possible-to-pass-integer-value-as-method-arguments-in-c-sharp-console-app

    Program adds +1 to the given integer and prints the new value to the console.
        <https://www.dotnetperls.com/increment>

    and appends the same number to a file on a new line (linefeed).
        Googlaa "dotnet append to file"

    Then proceeds to calls itself with the new integer value, but only if the command line argument value is less than 10. If not, program will both write "done" to the console and to the file.
        <https://medium.com/@david.bollobas/calling-another-process-from-c-console-app-david-dev-blog-9ee9b482ab1b>

        https://iq.direct/blog/51-how-to-get-the-current-executable-s-path-in-csharp.html