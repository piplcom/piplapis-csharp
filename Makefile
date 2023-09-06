# build the project inside the docker container (run from inside the container)
build:
	sh init.sh

# run tests on the built project (run from inside the container)
test:
	dotnet test -l "console;verbosity=detailed"

build-test: build test