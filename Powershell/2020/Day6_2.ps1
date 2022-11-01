$path = "D:\Personal Storage\Bas\Git\Scripts\AdventCode\2020"
#$data = Get-Content -Path "$($path)\Exampleday6_1"
$data = Get-Content -Path "$($path)\inputDay6_1"

[Collections.Generic.List[string[]]]$answergroups = @()


function Get-Alfabet() {
    $alphabet = New-Object pscustomobject
    for ([byte]$c = [char]'A'; $c -le [char]'Z'; $c++)  
    {  
        $alphabet | add-member -MemberType NoteProperty -name $([char]$c) -Value 0  
    }  
    return $alphabet
}

function Get-GivenAnswerByall{
    param(
        [int]
        $count,
        $alphabet
    )

    $allGivenAnswer = 0
    for ([byte]$c = [char]'A'; $c -le [char]'Z'; $c++)  
    {  
        if($alphabet.$([char]$c) -eq $count){
            $allGivenAnswer++
        }
    }  

    return $allGivenAnswer
}
 
#[String]::Join(", ", $alphabet) 

$answergroup = @()

foreach($line in $data){
    
    if($line -ne ""){
        $answergroup += [string]$line
    }elseif($line -eq ""){
        if($answergroup -ne ""){    
            $answergroups.add($answergroup)
        }
        $answergroup = @()
    }


}
#last one
if($answergroup -ne ""){    
    $answergroups.add($answergroup)
}

$Allgivenanswers = 0

foreach($answergroup in $answergroups){
    $alphabet = Get-Alfabet
    foreach($answerline in $answergroup){
        $string = $answerline.ToCharArray()
        $string = $string | Select-Object -Unique  
        foreach($letter in $string){
            $alphabet.$letter++
        }
        
    }
    $Allgivenanswers += Get-GivenAnswerByall -count $answergroup.count -alphabet $alphabet

}
