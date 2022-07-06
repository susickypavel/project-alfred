import { NextUIProvider } from "@nextui-org/react";

function App({ Component, pageProps }) {
  return (
    <NextUIProvider>
      <Component {...pageProps} />
    </NextUIProvider>
  );
}

export default App;
