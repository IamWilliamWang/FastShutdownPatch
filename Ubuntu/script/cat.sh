awk 'BEGIN { FS = "鋝"; OFS = "\n" }{ for(i = 1; i <= NF; i = i+1) print $i }' ./TimeDatabase.cache