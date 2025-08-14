import { useState, useEffect } from "react";
import type { Registro } from "../types/Registro";
import { getRegistrosByContribuyente, getTotalItbisSum } from "../api/registrosService";

export const useRegistros = (contribuyenteId: string) => {
  const [data, setData] = useState<Registro[]>([]);
  const [page, setPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [isLastPage, setIsLastPage] = useState(false);
  const [loading, setLoading] = useState(false);
  const [totalSum, setTotalSum] = useState<number>(0);

  useEffect(() => {
    if (!contribuyenteId) return;

    setLoading(true);

    Promise.all([
      getRegistrosByContribuyente(contribuyenteId, page),
      getTotalItbisSum(contribuyenteId)
    ]).then(([listResponse, sumResponse]) => {
      setData(listResponse.data);
      setTotalPages(listResponse.totalPages);
      setIsLastPage(listResponse.isLastPage);
      setTotalSum(sumResponse);
      setLoading(false);
    });

  }, [contribuyenteId, page]);

  return { data, page, setPage, totalPages, isLastPage, loading, totalSum };
};
