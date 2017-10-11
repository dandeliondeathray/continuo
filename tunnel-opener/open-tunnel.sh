#!/bin/ash

E_OPTERROR=1

usage="Usage: ./open-tunnel.sh -u=<url> -n=<name>

ARGUMENTS
  -u <url>    Url where to post the public url of the tunnel
  -n <name>   Name of the tunnel in the ngrok configuration"

while getopts "u:n:" option
do
  case $option in
    u)
      url="${OPTARG}";;
    n)
      name="${OPTARG}";;
    \?)
      echo -e "Unknown option: $option" 
      echo -e $usage
      exit $E_OPTERROR;;
  esac
  OPRIND=${OPTIND}
done

if [ -z ${url+x} ] || [ -z ${name+x} ]; then
  echo -e $usage
  exit $E_OPTERROR
fi

tunnel_url=`./wtfc.sh -T 3 --quiet nc -z ngrok 4040 && curl -s ngrok:4040/api/tunnels | jq ".tunnels[] | select(.name == \"$name\") | .public_url"`
echo "Posting tunnel url to '$name' with url $url: $tunnel_url"
./wtfc.sh -T 3 --quiet nc -z orchestration 80 && curl -s --header "Content-Type: application/json" --data-binary "$tunnel_url" $url
echo ""
