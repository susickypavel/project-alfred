import { Button } from "@mantine/core";
import { FaDiscord } from "react-icons/fa";

import type { NextPage } from "next";

const IndexPage: NextPage = () => {
  return (
    <main className="h-full w-full flex justify-center items-center">
      <Button
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
