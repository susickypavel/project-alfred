import { Button } from "@mantine/core";
import { FaDiscord } from "react-icons/fa";

import { DISCORD_AUTHORIZATION_URL } from "../src/utils/get-authorization-url";

import type { NextPage } from "next";

const IndexPage: NextPage = () => {
  return (
    <main className="h-full w-full flex justify-center items-center">
      <Button
        component="a"
        href={DISCORD_AUTHORIZATION_URL}
        rightIcon={<FaDiscord />}
        styles={{
          root: {
            backgroundColor: "#5865F2",
            fontWeight: 700,
          },
        }}
      >
        Discord Login
      </Button>
    </main>
  );
};

export default IndexPage;
