$path = "D:\Personal Storage\Bas\Git\Scripts\AdventCode\2020"
#$data = Get-Content -Path "$($path)\Exampleday6_1"
$data = Get-Content -Path "$($path)\inputDay6_1"

$answerlines = @()

foreach($line in $data){
    
    if($line -ne ""){
        $answerline += $line
    }elseif($line -eq ""){
        if($answerline -ne ""){    
            $answerlines += $answerline
        }
        $answerline = ""
    }
}
#last one
if($answerline -ne ""){    
    $answerlines += $answerline
}

$givenanswers = 0

foreach($answerline in $answerlines){

    $string = $answerline.ToCharArray()
    $string = $string | Select-Object -Unique  
    $givenanswers += $string.count
}
