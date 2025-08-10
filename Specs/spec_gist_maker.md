# Github Gist Creator

> Ingest the information from this file, implement the low-level tasks, and generate the code that will satisfiy the High and Mid-level objectives.

## High-Level Objectives

- Create a simple cli tool that creates gist on github given a directory of files and a description.

## Mid-Level Objectives

- Accept two cli args: Directory and Description like ```program.cs --path <directory> --description <description>```
- Create a private gist on github with the files in the directory

## Implementation Notes

- Be sure program.cs prints the progress and prints errors if needed

## Begining Context

- program.cs
- modules/gist.cs
- module/dataTypes.cs
- modules/http.cs

## Ending Context

- program.cs
- modules/gist.cs
- modules/dataTypes.cs
- modules/http.cs

## Low-Level Tasks

1. ```cs

CREATE modules/http.cs
    CREATE method Post(url, headers, body) -> dict or throw

UPDATE modules/dataTypes.cs
    CREATE method Files() to support the following structure:
    {"files": [
        "README.md": {"content": Helloworld}
    ]}
    CREATE method Gist() to support the following structure:
        {"description": "example of a gist", "public": false, "files":Files}
    CREATE method GistFile

CREATE modules/files.cs
    CREATE method pullFiles(directoryPath) -> files[] or throw

cs```
2. ```cs

CREATE modules/gist.cs
    CREATE method createGist(gist: Gist) -> dict or throw
        call modules/http.post(url, headers, body) -> dict or throw
    example code:
        curl -L \
        -X POST \
        -H "Accept: application/vnd.github+json \
        -H "Authorization: Bearer <YOUR-TOKEN>" \
        -H "X-Gihub-api-version: 2022-11-28" \
        https://api.gihub.com/gist \

cs```
3. ```cs
UPDATE program.cs
    CREATE method main() - > none
    parse cli args
    call modules/files.pullFiles(directoryPath) -> gistFiles[]
    call modules/gist.createGist(Gist) -> dict
    print progress
    print errors if needed
    call main
cs```
