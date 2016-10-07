
Module Code


    Public Function GetCulture(ByVal strReportLanguage As String) As System.Globalization.CultureInfo
        strReportLanguage = strReportLanguage.ToLower()

        Dim strCultureString As String = ""
        Select Case strReportLanguage
            Case "de"
                strCultureString = "de-ch"
            Case "fr"
                strCultureString = "fr-ch"
            Case "it"
                strCultureString = "it-ch"
            Case "en"
                strCultureString = "en-us"
            Case Else
                strCultureString = strReportLanguage
        End Select

        Return New System.Globalization.CultureInfo(strCultureString)
    End Function





    Public Function GetPageEnumeration(strFormatString As String, iPageNumber As Integer, iTotalNumberOfPages As Integer) As String
        ' Dim strFormatString As String = "Seite {0} von {1}"
        Return String.Format(strFormatString, iPageNumber, iTotalNumberOfPages)
    End Function


    Public Function GetDateForCulture(iDay As Integer, iMonth As Integer, iYear As Integer, strFormat As String, strReportLanguage As String) As String
        If String.IsNullOrEmpty(strReportLanguage) Then
            strReportLanguage = "DE"
        End If

        If String.IsNullOrEmpty(strFormat) Then
            strFormat = "d. MMMM yyy"
        End If

        Dim strCultureString As String = ""
        Select Case strReportLanguage.ToLower()
            Case "de"
                strCultureString = "de-ch"
                Exit Select
            Case "fr"
                strCultureString = "fr-ch"
                Exit Select
            Case "it"
                strCultureString = "it-ch"
                Exit Select
            Case "en"
                strCultureString = "en-us"
                Exit Select
            Case Else
                strCultureString = strReportLanguage
                Exit Select
        End Select
        ' End Switch strReportLanguage
        Dim ci As New System.Globalization.CultureInfo(strCultureString)
        Dim dt As New System.DateTime(iYear, iMonth, iDay)

        Dim strDateAsString As String = dt.ToString(strFormat, ci)
        strDateAsString = ci.TextInfo.ToTitleCase(strDateAsString)

        Return strDateAsString
    End Function ' GetDateForCulture



    Public Function GetDateForCulture(ByVal iDay As Integer, ByVal iMonthNum As Integer, ByVal iYear As Integer, ByVal strReportLanguage As String) As String
        Dim strReturnValue As String = ""
        Try
            strReportLanguage = strReportLanguage.ToLower()

            'Dim dtThisDateDate As DateTime = DateTime.Now
            'Dim dtThisDateDate As New DateTime(2010, 12, 31)
            Dim dtThisDateDate As New System.DateTime(iYear, iMonthNum, iDay)

            Dim strCultureString As String = ""
            Select Case strReportLanguage
                Case "de"
                    strCultureString = "de-ch"
                Case "fr"
                    strCultureString = "fr-ch"
                Case "it"
                    strCultureString = "it-ch"
                Case "en"
                    strCultureString = "en-us"
                Case Else
                    strCultureString = strReportLanguage
            End Select

            Dim ci As New System.Globalization.CultureInfo(strCultureString)

            Dim strFormat = ci.DateTimeFormat.LongDatePattern
            If ci.CompareInfo.IndexOf(strFormat, "dddd, ", System.Globalization.CompareOptions.IgnoreCase) > -1 Then
                strFormat = Mid(ci.DateTimeFormat.LongDatePattern, Len("dddd, ") + 1)
            End If

            strReturnValue = dtThisDateDate.ToString(strFormat, ci)
            'strReturnValue = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(strReturnValue)
            strReturnValue = ci.TextInfo.ToTitleCase(strReturnValue)
        Catch ex As System.Exception
            strReturnValue = iDay.ToString().PadLeft(2, "0") + "." + iMonthNum.ToString().PadLeft(2, "0") + "." + iYear.ToString()
        End Try

        Return strReturnValue
    End Function


    Function Concat(x As String, y As String, z As String) As String
        If String.IsNullOrEmpty(x) Then
            If String.IsNullOrEmpty(y) Then
                Return ""
            Else
                Return y
            End If
        Else
            If String.IsNullOrEmpty(y) Then
                Return x
            Else
                Return x + z + y
            End If
        End If

        Return ""
    End Function




    Public Function ReplacementCall(strReplaceInput As String) As String
        Return strReplaceInput.TrimStart("0"c)
    End Function


    Public Function RemoveNewLine(obj As Object) As String
        Return RemoveNewLine(obj, False)
    End Function ' RemoveNewLine


    Public Function RemoveNewLine(obj As Object, bWithRemoveRoom As Boolean) As String
        If obj Is Nothing Then
            Return " "
        End If

        Dim str As String = System.Convert.ToString(obj)

        If String.IsNullOrEmpty(str) Then
            Return " "
        End If


        'str = str.Replace("/", Environment.NewLine)
        str = str.Replace(vbCr, "")
        str = str.Replace(vbLf, "")
        str = str.Trim()


        If bWithRemoveRoom Then
            str = System.Text.RegularExpressions.Regex.Replace(str, "^RM", "")
            str = System.Text.RegularExpressions.Regex.Replace(str, "\.00$", "")
        End If

        If System.Text.RegularExpressions.Regex.IsMatch(str, "^0+$") Then
            str = "0"
        Else
            If str IsNot Nothing Then
                Dim pattern As String = "^[-]?\s*(?<ID>[0-9]+)\s*$"
                Dim regex As New System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase Or System.Text.RegularExpressions.RegexOptions.Multiline)

                str = regex.Replace(str, New System.Text.RegularExpressions.MatchEvaluator(AddressOf ReplaceLeadingZeros))
            End If

        End If

        str = NoEmptyString(str)

        Return str
    End Function ' RemoveNewLine


    Function NoEmptyString(obj As Object) As String
        If obj Is Nothing Then
            Return " "
        End If

        Dim str As String = System.Convert.ToString(obj)

        If String.IsNullOrEmpty(str) Then
            Return " "
        End If

        Return str
    End Function


    Public Function ReplaceLeadingZeros(m As System.Text.RegularExpressions.Match) As String
        Dim retVal As String = m.Value
        Dim capt As System.Text.RegularExpressions.Capture = m.Groups("ID")


        If capt Is Nothing Then
            Return retVal
        End If

        Dim ind As Integer = capt.Index - m.Index
        Dim sb As New System.Text.StringBuilder(retVal)
        sb.Remove(ind, capt.Length)
        sb.Insert(ind, capt.Value.TrimStart("0"c))
        retVal = sb.ToString()
        sb.Length = 0
        sb = Nothing

        Return retVal
    End Function


End Module
