import React, {useEffect, useState} from "react";
import {StatRowProps} from "./StatRowProps";
import axios from "axios";
import {API_URL} from "../../constants";


const Stats: React.FC = () => {

    const [stats, setStats] = useState([] as Array<StatRowProps>)

    useEffect(() => {

        async function fetchInfo() {
            const res = await axios.get(`${API_URL}/statistics`, { withCredentials: true });

            if (res.status === 200) {
                setStats(res.data)
            }
        }

        fetchInfo().then();

        setStats([{
            id: 0,
            name: "artem",
            count: 1,
            author: "meh"
        }])
    }, [])

    return (
        <div className="container flex flex-col mx-auto">
            <table className="table-fixed w-11/12 md:w-1/2 mx-auto border-collapse">
                <thead>
                <tr>
                    <th className="w-1/5 border-2 border-indigo-600 ">№</th>
                    <th className="w-2/5 border-2 border-indigo-600 ">Имя</th>
                    <th className="w-1/5 border-2 border-indigo-600 ">Количество</th>
                    <th className="w-2/5 border-2 border-indigo-600 ">Автор</th>
                </tr>
                </thead>
                <tbody>
                { stats.map((e) => {
                    return (
                        <tr key={ e.id }>
                            <td className="border border-indigo-600 ">{ e.id }</td>
                            <td className="border border-indigo-600 ">{ e.name }</td>
                            <td className="border border-indigo-600 ">{ e.count }</td>
                            <td className="border border-indigo-600 ">{ e.author }</td>
                        </tr>
                    )
                }) }
                </tbody>
            </table>
        </div>

    )
}

export default Stats;
