import useStore from "@/store/store";
import { useState, useEffect } from "react";

interface ApiConfig {
  petUrl: string; // URL for fetching pet data
  secondaryUrl?: string; // URL for fetching missing data (optional)
}
// TODO: change the name of the interface
interface PetVaccineData {
  data: unknown;
  secondaryData?: unknown;
  isLoading: any;
  refreshData: any;
}

const useFetchData = (id: number, config: ApiConfig): PetVaccineData => {
  const { token } = useStore();
  const tokenValue = token();
  const [data, setData] = useState<unknown>(null);
  const [secondaryData, setSecondaryData] = useState<unknown | undefined>(
    undefined,
  ); // Optional
  const [isLoading, setLoading] = useState(true);
  const [refreshKey, setRefreshKey] = useState(0);

  useEffect(() => {
    setLoading(true);

    const fetchData = async () => {
      const controller = new AbortController();
      const signal = controller.signal;
      const promises = [
        fetch(config.petUrl.replace("{id}", id.toString()), {
          signal,
          headers: {
            Authorization: `Bearer ${tokenValue}`, // Set the authorization header
          },
        }),
      ];
      if (config.secondaryUrl) {
        promises.push(fetch(config.secondaryUrl, { signal }));
      }

      setTimeout(() => {
        Promise.all(promises)
          .then(async (responses) => {
            const [res1, res2] = responses;

            if (!res1.ok || (res2 && !res2.ok)) {
              throw new Error("Failed to fetch data");
            }

            const data = await res1.json();
            const secondaryData = res2 ? await res2.json() : undefined; // Handle optional second response
            setData(data);
            setSecondaryData(secondaryData);
            setLoading(false);
          })
          .catch((error) => {
            console.error("Error fetching data:", error);
            setLoading(false);
          });
      }, 500);
    };

    fetchData();
  }, [
    id,
    refreshKey,
    config.petUrl,
    config.secondaryUrl,
    config.authorizationToken,
  ]);

  const refreshData = () => setRefreshKey((oldKey) => oldKey + 1);

  return { data, secondaryData, isLoading, refreshData };
};

export default useFetchData;
