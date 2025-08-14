import Pagination from "./Pagination";
import  LoadingSpinner  from "./LoadingSpinner";
import type { Registro } from "../types/Registro";

interface Props {
  data: Registro[];
  page: number;
  totalPages: number;
  loading: boolean;
  onPageChange: (page: number) => void;
  totalSum: number;
}

export const RegistrosTable = ({ data, page, totalPages, loading, onPageChange, totalSum }: Props) => {
  if (loading) return <LoadingSpinner />;

  return (
    <div style={{ padding: "1rem", height: "100%", display: "flex", flexDirection: "column" }}>
      <h3>Total ITBIS: {totalSum.toLocaleString()}</h3>
      <table style={{ width: "100%", borderCollapse: "collapse" }}>
        <thead>
          <tr>
            <th>NCF</th>
            <th>Monto</th>
            <th>ITBIS 18%</th>
          </tr>
        </thead>
        <tbody>
          {data.map((r, idx) => (
            <tr key={idx}>
              <td>{r.ncf}</td>
              <td>{r.monto.toLocaleString()}</td>
              <td>{r.itebis18.toLocaleString()}</td>
            </tr>
          ))}
        </tbody>
      </table>
      <Pagination page={page} totalPages={totalPages} onChange={onPageChange} />
    </div>
  );
};
