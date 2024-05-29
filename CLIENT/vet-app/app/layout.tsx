import type { Metadata } from "next";
import "./globals.css";
import Navbar from "./nav/Navbar";
import { Toaster } from "@/components/ui/toaster";
import TanstackProvider from "@/components/TanstackProvider";
import NextTopLoader from 'nextjs-toploader';
import { cookies } from "next/headers";

export const metadata: Metadata = {
  title: "vet-client",
  description: "",
};

export default function RootLayout({

  children,
}: Readonly<{
  children: React.ReactNode;
}>) {

  let kookies = cookies().has("token");
  return (
    <html lang="en">
      <body style={{ minWidth: "98vw", maxWidth: "100%", height: "90vh" }}>
        <TanstackProvider>
          <Navbar />
          <main className="{kookies == false ? p-0 : p-5}" style={{ minWidth: "98%", maxHeight: "90vh" }}>
            <NextTopLoader color="#4edd22" height={5} />
            {children}
          </main>
          <Toaster />
        </TanstackProvider>
      </body>
    </html>
  );
}
