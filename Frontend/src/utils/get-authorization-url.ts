// TODO: Add State
const DISCORD_BASE_AUTHORIZATION_URL =
  "https://discordapp.com/api/oauth2/authorize";
const SCOPE = "identify";

export const DISCORD_AUTHORIZATION_URL = `${DISCORD_BASE_AUTHORIZATION_URL}?client_id=${process.env.NEXT_PUBLIC_DISCORD_CLIENT_ID}&redirect_uri=${process.env.NEXT_PUBLIC_AUTHORIZATION_REDIRECT}&response_type=code&scope=${SCOPE}`;
