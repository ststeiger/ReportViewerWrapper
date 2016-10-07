
Module Module1


    Sub Main()
        Dim str As String = Code.NoEmptyString("foo")
        str = Code.NoEmptyString("")

        System.Console.WriteLine(str)
    End Sub


End Module
