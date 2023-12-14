#!/bin/bash

# Usage: ./build_sitemap.sh /path/to/your/folder https://www.scichart.com/documentation/ios/current/

folderPath=$1
rootUrl=$2

# Check if the folder path parameter is provided
if [ -z "$folderPath" ]; then
    echo "Please provide the folder path as a parameter, e.g., /path/to/your/folder"
    exit 1
fi

# Check if the root URL path parameter is provided
if [ -z "$rootUrl" ]; then
    echo "Please provide the root URL as a parameter, e.g., https://www.mywebsite.com/"
    exit 1
fi

# Remove any trailing slash from rootUrl if it exists
rootUrl=${rootUrl%/}

sitemapEntries=()
today=$(date +"%Y-%m-%dT%H:%M:%S+00:00")
itemCount=0

# Loop through HTML files in the folder
while IFS= read -r filePath; do
    # filename=$(basename "$filePath")
    filename="${filePath#$folderPath/}"

    # URL encode filename
    encodedFilename=$(python3 -c "import urllib.parse; print(urllib.parse.quote('''$filename'''))")

    # Build the URL by replacing backslashes with forward slashes
    url="${rootUrl}/${encodedFilename}"

    # Create a sitemap entry
    sitemapEntry="    <url>
        <loc>$url</loc>
        <lastmod>$today</lastmod>
        <changefreq>weekly</changefreq>
        <priority>0.5</priority>
    </url>"
    
    # Add the entry to the array
    sitemapEntries+=("$sitemapEntry")
    itemCount=$((itemCount+1))

    echo "Adding entry $url"
done < <(find "$folderPath" -name '*.html')

# Create the sitemap.xml content
sitemapContent="<?xml version=\"1.0\" encoding=\"UTF-8\"?>
<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">
$(printf '%s\n' "${sitemapEntries[@]}")
</urlset>"

echo "Creating sitemap: $folderPath/sitemap.xml with $itemCount items"

# Save the sitemap.xml content to a file
echo "$sitemapContent" > "$folderPath/sitemap.xml"
