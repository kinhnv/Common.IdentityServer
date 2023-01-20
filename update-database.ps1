Set-Variable -Name startupProjectFolder -Value "UserService.Api"
Set-Variable -Name startupProjectFile -Value $startupProjectFolder".csproj"
Set-Variable -Name startupProject -Value "$startupProjectFolder\$startupProjectFile"
Set-Variable -Name startupProjectTemp -Value $startupProject"_temp"
Set-Variable -Name projectFolder -Value "UserService.Infrastructure"
Set-Variable -Name projectFile -Value $projectFolder".csproj"
Set-Variable -Name project -Value "$projectFolder\$projectFile"
Set-Variable -Name attributeName -Value "ReferenceOutputAssembly"
Set-Variable -Name attributeValueFrom -Value "False"
Set-Variable -Name attributeValueTo -Value "True"

Get-Content $startupProject | %{$_ -replace "<$attributeName>$attributeValueFrom</$attributeName>","<$attributeName>$attributeValueTo</$attributeName>"} > $startupProjectFile
mv $startupProject $startupProjectTemp
mv $startupProjectFile $startupProject

dotnet ef database update -p $project -s $startupProject

rm $startupProject
mv $startupProjectTemp $startupProject