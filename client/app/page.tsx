import styles from './page.module.css'
import PageCarousel from "@/app/components/pageCarousel/PageCarousel";

export default function Home() {


    return (
        <main className={styles.page}>
            <PageCarousel/>
        </main>
    )
}
