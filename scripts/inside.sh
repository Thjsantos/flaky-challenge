#!/usr/bin/env bash
# Runs inside the Playwright container: builds the tests and runs them REPEAT times
# against the live site. Extra args go to `dotnet test`.
set -u
cd "$(dirname "$0")/.."
REPEAT="${REPEAT:-1}"

dotnet build Tests/Tests.csproj -nologo -v q || exit 1

pass=0; fail=0
for run in $(seq 1 "$REPEAT"); do
  echo "=== run $run/$REPEAT ==="
  if dotnet test Tests/Tests.csproj --no-build -nologo "$@"; then pass=$((pass+1)); else fail=$((fail+1)); fi
done
[ "$REPEAT" -gt 1 ] && echo "SUMMARY: $pass green runs, $fail red runs out of $REPEAT"
[ "$fail" -eq 0 ]
