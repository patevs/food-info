# ----------- #
# src/run.ps1 #
# ----------- #

# Compile and execute a C# program

# --------------------------------------------------------------------------

# Global Constants

# C# Compiler
# $csc = "C:\Windows\Microsoft.NET\Framework\v3.5\csc.exe"
$csc = "C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe"

$filename = "Food"

# --------------------------------------------------------------------------

# Helper Functions

# Check if a given command exists
# https://stackoverflow.com/questions/3919798/how-to-check-if-a-cmdlet-exists-in-powershell-at-runtime-via-script
function CheckCommand($cmdname) {
    # return [bool](Get-Command -Name $cmdname -ea 0)
    return [bool](Get-Command -Name $cmdname -ErrorAction SilentlyContinue)
}

# Compile the program
function compile {
  # $command = "$csc /nologo /t:exe /out:Food.exe Food.cs"
  $command = "$csc /nologo /t:exe /out:$filename.exe $filename.cs"
  Invoke-Expression $command
}

# Execute the program
function execute {
  # $command = "./Food"
  $command = "./$filename"
  Invoke-Expression $command
}

# --------------------------------------------------------------------------

Write-Host @'

  Compiling Program
  -----------------
'@

Write-Host "  * Detecting C# Compiler Installation... " # -NoNewLine

# Verify C# compiler is installed and available

if (CheckCommand $csc){
    # https://stackoverflow.com/questions/6338015/how-do-you-execute-an-arbitrary-native-command-from-a-string
    $command = "$csc /help | grep 'Compiler version'"
    $cscVersion = Invoke-Expression $command # Can use iex instead of Invoke-Expression
    Write-Host "  * C# Compiler: " -NoNewLine
    Write-Host $cscVersion -ForegroundColor Green # -NoNewLine
} else {
    Write-Host "`n`n C# Compiler Installation Could Not Be Found! Exiting..." -ForegroundColor Red
    exit
}

Write-Host @'

  Executing Program
  -----------------
'@

# Compile and run the program

compile
execute

# EOF #
