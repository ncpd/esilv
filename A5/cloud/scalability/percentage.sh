wc -l employees.log | cut -d' ' -f1 | awk '{printf "%.3f %%", 100*$1/300024}' ; echo
