#!/bin/bash

# Check if docfx is installed
if ! [ -x "$(command -v jazzy)" ]; then
  echo 'Error: jazzy is not installed' >&2
  echo 'You can install it with `[sudo] gem install jazzy` or from https://github.com/realm/jazzy#installation' >&2
  exit 1
fi
# Check if sourcekitten is installed
if ! [ -x "$(command -v sourcekitten)" ]; then
  echo 'Error: sourcekitten is not installed' >&2
  echo 'You can install it with `brew install sourcekitten` or from https://github.com/jpsim/SourceKitten#installation' >&2
  exit 1
fi

SciChart="SciChart"
Framework="$SciChart.framework"
XCFramework="$SciChart.xcframework"

SCRIPT_DIR="`pwd`/`dirname "$0"`"
JAZZY_PROJ_PATH=${SCRIPT_DIR%"/."}
JAZZY_PROJ_PATH=${JAZZY_PROJ_PATH%"/build"}
DEFAULT_FRAMEWORK_PATH="$JAZZY_PROJ_PATH/$XCFramework"

usage() {
cat <<EOF
Usage: sh $0 command [argument(s)]

command:
  generate-api-docs <path_to_framework> :  generates api documentation from pre-built \`SciChart.framework\` or \`SciChart.xcframework\`.
  generate-site                         :  generates full documentation site from \`.md\` articles and \`SourceKit\` API docs metadata.
  build <path_to_framework>             :  build full documentation from \`.md\` articles and SciChart.iOS API docs comments.
params:
  <path_to_framework>                   :  if there's no path provided, script will look in the documentation root directory.

EOF
}

generate_api_docs() {
    echo $1
    SCICHART_PATH=${1:-$DEFAULT_FRAMEWORK_PATH}
    if [[ $SCICHART_PATH != /* ]]; then
        # if path is relative - prepend `pwd`
        SCICHART_PATH=$PWD/$SCICHART_PATH  
    fi
    if [ ! -d $SCICHART_PATH ]; then
        echo "Directory $SCICHART_PATH DOES NOT exists." >&2
        exit 1
    fi

    if [[ "$SCICHART_PATH" == *"$XCFramework" ]]; then
        # in case of .xcframework - select folder which contains single .framework (any arch is sufficient)
        pushd $SCICHART_PATH
        for folder in *; do
            if [[ $folder =~ ios-.* ]]; then
                PATH_TO_FRAMEWORK=$folder
                break
            fi
        done
        popd
        PATH_TO_FRAMEWORK=$SCICHART_PATH/$PATH_TO_FRAMEWORK/$Framework
    elif [[ "$SCICHART_PATH" == *"$Framework" ]]; then
        PATH_TO_FRAMEWORK=$SCICHART_PATH
    else
        echo "<path_to_framework> should point either to **/$Framework or to **/$XCFramework." >&2
        exit 1        
    fi

    DOCS_HEADERS_DIR="$JAZZY_PROJ_PATH/api/docs-headers"
    mkdir -p $DOCS_HEADERS_DIR
    
    pushd $DOCS_HEADERS_DIR
    # SciChart.h must be on the same level, that it's headers SciChart/*.h
    cp -a $PATH_TO_FRAMEWORK/Headers/. $SciChart/
    mv $SciChart/SciChart.h ./

    echo " ----- Generating \`SourceKit\` API files..."
    sourcekitten doc --objc "SciChart.h" -- -x objective-c \
        -isysroot $(xcrun --sdk iphonesimulator --show-sdk-path) \
        -I "./" > ../scichart-sourcekit-api-docs
    echo " ----- Finished generating \`SourceKit\` API files..."

    popd
    echo " ----- Cleaning up after temp headers dir..."
    rm -rf $DOCS_HEADERS_DIR
}

generate_site() {
    echo " ----- Generating static website combining API from SourceKit files and articles..."
    # jazzy config file is .jazzy.yaml by default
    pushd $JAZZY_PROJ_PATH
    jazzy
    popd
}

build() {
    generate_api_docs $1
    generate_site
}

# Read and select command to execute
COMMAND=$1

case $COMMAND in
    "generate-api-docs")
        generate_api_docs $2
        exit 0
        ;;

    "generate-site")
        generate_site
        exit 0
        ;;

    "build")
        build $2
        exit 0
        ;;
        
    *)
        echo "ERROR: Unknown command '$COMMAND'"
        usage
        exit 1
        ;;
esac
