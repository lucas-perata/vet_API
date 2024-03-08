import type { Metadata } from "next";
import "./globals.css";
import Navbar from "./nav/Navbar";
import { Toaster } from "@/components/ui/toaster";
import TanstackProvider from "@/components/TanstackProvider";

export const metadata: Metadata = {
  title: "vet-client",
  description: "Generated by create next app",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body style={{ minWidth: "100vw", height: "90vh" }}>
        <TanstackProvider>
          <Navbar />
          <main
            className="p-10"
            style={{ minWidth: "100%", minHeight: "90vh" }}
          >
            {children}
          </main>
          <Toaster />
        </TanstackProvider>
      </body>
    </html>
  );
}
