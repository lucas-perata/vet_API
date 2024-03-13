import React from "react";
import { PieChart, Pie, Cell, Tooltip } from "recharts"; // Assuming you're using Recharts

type Props = {
  data: any;
};

export default function CategorySumChart({ data }: Props) {
  const COLORS = [
    "#00C81F", // Forest Green (Calming & Trustworthy)
    "#FFD700", // Gold (Playful & Energetic)
    "#4169E1", // Royal Blue (Professional & Reliable)
    "#F0E68C", // Light Yellow (Compassionate & Caring)
    "#FF00FF", // Magenta (Nature-Inspired)
  ];

  return (
    <PieChart width={800} height={400}>
      {data.length === 0 ? (
        <p>No data to display</p>
      ) : (
        <Pie
          data={data}
          cx={120}
          cy={200}
          innerRadius={60}
          outerRadius={80}
          fill=""
          paddingAngle={5}
          dataKey="total"
          label={(dataPoint) => dataPoint.categoryName} // Access categoryName directly
        >
          {data.map((item, index) => (
            <Cell
              key={`cell-${item.categoryName}`}
              fill={COLORS[index % COLORS.length]}
            />
          ))}
        </Pie>
      )}
    </PieChart>
  );
}
