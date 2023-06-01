using MasterTestingV2.APIModels;
using System.Xml.Serialization;

namespace MasterTestingV2
{
    public class Grouping
    {
        private Random rng = new();

        public void CourseOneGroups(APIDbContext db)
        {
            var reviews = db.TextReviews.Where(n => n.Course == 1).ToArray();

            CombineIntoOneGroup(reviews, new[] { 1, 3 }, 1);
            CombineIntoOneGroup(reviews, new[] { 2, 4 }, 2);
            CombineIntoOneGroup(reviews, new[] { 5 }, 3);
            CombineIntoOneGroup(reviews, new[] { 6 }, 4);
            CombineIntoOneGroup(reviews, new[] { 7 }, 5);
            CombineIntoOneGroup(reviews, new[] { 8, 9 }, 6);
            CombineIntoOneGroup(reviews, new[] { 10, 11 }, 7);
            CombineIntoOneGroup(reviews, new[] { 12, 13 }, 8);
            CombineIntoOneGroup(reviews, new[] { 14, 15 }, 9);
            CombineIntoOneGroup(reviews, new[] { 16, 17 }, 10);
            CombineIntoOneGroup(reviews, new[] { 18, 19 }, 11);
            CombineIntoOneGroup(reviews, new[] { 20, 21 }, 12);
            CombineIntoOneGroup(reviews, new[] { 22, 23, 24 }, 13);
            CombineIntoOneGroup(reviews, new[] { 25, 26, 27, 28 }, 14);
            CombineIntoOneGroup(reviews, new[] { 29, 30, 31, 32, 33 }, 15);
            CombineIntoOneGroup(reviews, new[] { 34, 35, 36, 37, 38, 39, 40, 41, 42 }, 16);
            CombineIntoOneGroup(reviews, new[] { 43, 44, 45, 46, 47, 48, 49, 50, 52, 53, 54 }, 17);
            CombineIntoOneGroup(reviews, new[] { 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 66, 67, 68, 71, 72, 73, 74, 75 }, 18);
            CombineIntoOneGroup(reviews, new[] { 76, 78, 81, 82, 83, 84, 85, 88, 89, 90, 93, 95, 98, 99, 103, 104, 105, 107, 112, 114, 116, 144, 154, 186, 203, 221, 225 }, 19);

            db.SaveChanges();
        }

        public void CourseTwoGroups(APIDbContext db)
        {
            var reviews = db.TextReviews.Where(n => n.Course == 2).ToArray();

            CombineIntoOneGroup(reviews, new[] { 1, 2, 3 }, 1);
            CombineIntoOneGroup(reviews, new[] { 4 }, 2);
            CombineIntoOneGroup(reviews, new[] { 5 }, 3);
            CombineIntoOneGroup(reviews, new[] { 6 }, 4);
            CombineIntoOneGroup(reviews, new[] { 7 }, 5);
            CombineIntoOneGroup(reviews, new[] { 8, 11 }, 6);
            CombineIntoOneGroup(reviews, new[] { 9, 10 }, 7);
            CombineIntoOneGroup(reviews, new[] { 12 }, 8);
            CombineIntoOneGroup(reviews, new[] { 13, 14 }, 9);
            CombineIntoOneGroup(reviews, new[] { 15, 16 }, 10);
            CombineIntoOneGroup(reviews, new[] { 17, 18 }, 11);
            CombineIntoOneGroup(reviews, new[] { 19, 20 }, 12);
            CombineIntoOneGroup(reviews, new[] { 21, 22 }, 13);
            CombineIntoOneGroup(reviews, new[] { 21, 22 }, 14);
            CombineIntoOneGroup(reviews, new[] { 23, 24, 25, 26, 27 }, 15);
            CombineIntoOneGroup(reviews, new[] { 28, 29, 30, 31, 32 }, 16);
            CombineIntoOneGroup(reviews, new[] { 33, 34, 35, 36, 37 }, 17);
            CombineIntoOneGroup(reviews, new[] { 38, 39, 40, 41, 42, 43, 44, 46, 47, 48, 49, 50 }, 18);
            CombineIntoOneGroup(reviews, new[] { 52, 53, 54, 55, 58, 59, 60, 61, 62, 63, 64 }, 19);
            CombineIntoOneGroup(reviews, new[] { 65, 66, 69, 70, 72, 74, 76, 78, 81, 83, 84, 86, 89, 93, 96, 97, 98, 99, 100, 110, 114, 117, 118, 157, 179, 181, 183, 192, 350 }, 19);

            db.SaveChanges();
        }

        public void CourseThreeGroups(APIDbContext db)
        {
            var reviews = db.TextReviews.Where(n => n.Course == 3).ToArray();

            CombineIntoOneGroup(reviews, new[] { 1, 3 }, 1);
            CombineIntoOneGroup(reviews, new[] { 2, 4 }, 2);
            CombineIntoOneGroup(reviews, new[] { 5 }, 3);
            CombineIntoOneGroup(reviews, new[] { 6 }, 4);
            CombineIntoOneGroup(reviews, new[] { 7 }, 5);
            CombineIntoOneGroup(reviews, new[] { 8 }, 6);
            CombineIntoOneGroup(reviews, new[] { 9 }, 7);
            CombineIntoOneGroup(reviews, new[] { 10 }, 8);
            CombineIntoOneGroup(reviews, new[] { 11 }, 9);
            CombineIntoOneGroup(reviews, new[] { 12 }, 10);
            CombineIntoOneGroup(reviews, new[] { 13, 14 }, 11);
            CombineIntoOneGroup(reviews, new[] { 15, 16 }, 12);
            CombineIntoOneGroup(reviews, new[] { 17, 18 }, 13);
            CombineIntoOneGroup(reviews, new[] { 19, 20, 21 }, 14);
            CombineIntoOneGroup(reviews, new[] { 22, 23, 24, 25 }, 15);
            CombineIntoOneGroup(reviews, new[] { 26, 27, 28, 29 }, 16);
            CombineIntoOneGroup(reviews, new[] { 30, 31, 32, 33, 34, 35, 36 }, 17);
            CombineIntoOneGroup(reviews, new[] { 37, 38, 39, 40, 41, 42, 43, 44, 45, 46 }, 18);
            CombineIntoOneGroup(reviews, new[] { 47, 48, 49, 50, 51, 52, 54, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65, 67, 68 }, 19);
            CombineIntoOneGroup(reviews, new[] { 69, 70, 71, 72, 73, 74, 78, 79, 81, 84, 85, 86, 90, 93, 96, 100, 101, 105, 107, 108, 114, 120, 121, 129, 132, 133, 141, 143, 145, 172, 178, 185, 195, 407 }, 19);

        }

        public void CourseFourGroups(APIDbContext db)
        {
            var reviews = db.TextReviews.Where(n => n.Course == 4).ToArray();

            CombineIntoOneGroup(reviews, new[] { 1, 2, 3 }, 1);
            CombineIntoOneGroup(reviews, new[] { 4, 5 }, 2);
            CombineIntoOneGroup(reviews, new[] { 6 }, 3);
            CombineIntoOneGroup(reviews, new[] { 7 }, 4);
            CombineIntoOneGroup(reviews, new[] { 8, 10 }, 5);
            CombineIntoOneGroup(reviews, new[] { 11, 9 }, 6);
            CombineIntoOneGroup(reviews, new[] { 12, 13 }, 7);
            CombineIntoOneGroup(reviews, new[] { 14, 15 }, 8);
            CombineIntoOneGroup(reviews, new[] { 16, 17 }, 9);
            CombineIntoOneGroup(reviews, new[] { 18, 19 }, 10);
            CombineIntoOneGroup(reviews, new[] { 20, 21, 23 }, 11);
            CombineIntoOneGroup(reviews, new[] { 22, 24, 25 }, 12);
            CombineIntoOneGroup(reviews, new[] { 26, 27, 28 }, 13);
            CombineIntoOneGroup(reviews, new[] { 29, 30, 31, 32, 33, 34 }, 14);
            CombineIntoOneGroup(reviews, new[] { 35, 36, 37, 38, 39, 40, 41 }, 15);
            CombineIntoOneGroup(reviews, new[] { 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53 }, 16);
            CombineIntoOneGroup(reviews, new[] { 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 70, 71, 72, 73 }, 17);
            CombineIntoOneGroup(reviews, new[] { 75, 79, 81, 83, 84, 85, 86, 89, 91, 92, 93, 99, 101, 114, 119, 121, 130, 135, 137, 140, 167, 183, 189, 191, 224, 227, 245 }, 18);

            db.SaveChanges();
        }

        public void CourseFiveGroups(APIDbContext db)
        {
            var reviews = db.TextReviews.Where(n => n.Course == 5).ToArray();

            CombineIntoOneGroup(reviews, new[] { 1, 2, 3 }, 1);
            CombineIntoOneGroup(reviews, new[] { 4, 5 }, 2);
            CombineIntoOneGroup(reviews, new[] { 6, 7, 8, 9 }, 3);
            CombineIntoOneGroup(reviews, new[] { 10, 11, 12, 13, 14 }, 4);
            CombineIntoOneGroup(reviews, new[] { 15, 16, 17, 18 }, 5);
            CombineIntoOneGroup(reviews, new[] { 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 }, 6);
            CombineIntoOneGroup(reviews, new[] { 29, 30, 31, 32, 33, 34, 35, 36, 37, 39, 41, 42, 43, 44, 45 }, 7);
            CombineIntoOneGroup(reviews, new[] { 48, 49, 50, 51, 53, 59, 63, 66, 68, 74, 76, 78, 81, 86, 89, 93, 100, 116, 117, 122, 123, 127, 130, 148, 248 }, 8);

            db.SaveChanges();
        }

        public void CourseSixGroups(APIDbContext db)
        {
            var reviews = db.TextReviews.Where(n => n.Course == 6).ToArray();

            CombineIntoOneGroup(reviews, new[] { 1, 2, 3, 5 }, 1);
            CombineIntoOneGroup(reviews, new[] { 4, 6 }, 2);
            CombineIntoOneGroup(reviews, new[] { 7, 8 }, 3);
            CombineIntoOneGroup(reviews, new[] { 9, 10 }, 4);
            CombineIntoOneGroup(reviews, new[] { 11, 22 }, 5);
            CombineIntoOneGroup(reviews, new[] { 12, 14 }, 6);
            CombineIntoOneGroup(reviews, new[] { 13, 15 }, 7);
            CombineIntoOneGroup(reviews, new[] { 16, 17 }, 8);
            CombineIntoOneGroup(reviews, new[] { 18, 19, 20 }, 9);
            CombineIntoOneGroup(reviews, new[] { 21, 23, 24, 25, 26 }, 10);
            CombineIntoOneGroup(reviews, new[] { 27, 28, 29, 30 }, 11);
            CombineIntoOneGroup(reviews, new[] { 31, 32, 33, 34, 35 }, 12);
            CombineIntoOneGroup(reviews, new[] { 36, 37, 38, 39, 40, 41 }, 13);
            CombineIntoOneGroup(reviews, new[] { 42, 43, 44, 45, 46, 47, 48 }, 14);
            CombineIntoOneGroup(reviews, new[] { 49, 50, 51, 52, 53, 54, 55, 56 }, 15);
            CombineIntoOneGroup(reviews, new[] { 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71 }, 16);
            CombineIntoOneGroup(reviews, new[] { 72, 73, 74, 75, 76, 78, 79, 80, 82, 83, 85, 86, 87, 88, 89, 90 }, 17);
            CombineIntoOneGroup(reviews, new[] { 93, 95, 98, 99, 100, 102, 107, 108, 113, 115, 116, 117, 120, 121, 127, 130, 135, 139, 143, 181, 195, 278 }, 18);

            db.SaveChanges();
        }

        public void CourseSevenGroups(APIDbContext db)
        {
            var reviews = db.TextReviews.Where(n => n.Course == 7).ToArray();

            CombineIntoOneGroup(reviews, new[] { 1, 2, 3, 6 }, 1);
            CombineIntoOneGroup(reviews, new[] { 4, 5 }, 2);
            CombineIntoOneGroup(reviews, new[] { 7, 8 }, 3);
            CombineIntoOneGroup(reviews, new[] { 9 }, 4);
            CombineIntoOneGroup(reviews, new[] { 10, 11 }, 5);
            CombineIntoOneGroup(reviews, new[] { 12, 13 }, 6);
            CombineIntoOneGroup(reviews, new[] { 14, 15, 16 }, 7);
            CombineIntoOneGroup(reviews, new[] { 17, 18 }, 8);
            CombineIntoOneGroup(reviews, new[] { 19, 20, 21 }, 9);
            CombineIntoOneGroup(reviews, new[] { 22, 23, 24 }, 10);
            CombineIntoOneGroup(reviews, new[] { 25, 26, 27, 28 }, 11);
            CombineIntoOneGroup(reviews, new[] { 29, 30, 31, 32 }, 12);
            CombineIntoOneGroup(reviews, new[] { 33, 34, 35, 36, 37, 40 }, 13);
            CombineIntoOneGroup(reviews, new[] { 38, 39, 41, 42, 43, 44 }, 14);
            CombineIntoOneGroup(reviews, new[] { 45, 46, 47, 48, 49, 50, 51 }, 15);
            CombineIntoOneGroup(reviews, new[] { 52, 54, 55, 56, 57, 58, 60, 61, 62, 63, 64, 65, 66, 67, 68, 71 }, 16);
            CombineIntoOneGroup(reviews, new[] { 72, 73, 74, 76, 77, 78, 79, 80, 81, 84, 85, 86, 89, 90, 93, 94, 98, 102, 103, 111, 112, 116, 118, 123, 124, 126, 136, 137, 173, 175 }, 17);

            db.SaveChanges();
        }

        public void CourseEightGroups(APIDbContext db)
        {
            var reviews = db.TextReviews.Where(n => n.Course == 8).ToArray();

            CombineIntoOneGroup(reviews, new[] { 1, 3, 4 }, 1);
            CombineIntoOneGroup(reviews, new[] { 2, 5 }, 2);
            CombineIntoOneGroup(reviews, new[] { 6 }, 3);
            CombineIntoOneGroup(reviews, new[] { 7, 8 }, 4);
            CombineIntoOneGroup(reviews, new[] { 9, 11 }, 5);
            CombineIntoOneGroup(reviews, new[] { 10, 12 }, 6);
            CombineIntoOneGroup(reviews, new[] { 13, 15 }, 7);
            CombineIntoOneGroup(reviews, new[] { 14, 16 }, 8);
            CombineIntoOneGroup(reviews, new[] { 17, 18, 19, 21 }, 9);
            CombineIntoOneGroup(reviews, new[] { 20, 22, 23 }, 10);
            CombineIntoOneGroup(reviews, new[] { 24, 25, 27 }, 11);
            CombineIntoOneGroup(reviews, new[] { 26, 28, 29, 30 }, 12);
            CombineIntoOneGroup(reviews, new[] { 31, 32, 33, 34 }, 13);
            CombineIntoOneGroup(reviews, new[] { 35, 36, 37, 38, 39 }, 14);
            CombineIntoOneGroup(reviews, new[] { 40, 41, 42, 43, 44, 45, 46 }, 15);
            CombineIntoOneGroup(reviews, new[] { 47, 48, 49, 50, 51, 52, 54, 55 }, 16);
            CombineIntoOneGroup(reviews, new[] { 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68 }, 17);
            CombineIntoOneGroup(reviews, new[] { 69, 71, 72, 73, 74, 75, 76, 77, 78, 80, 81, 83, 84, 85, 87, 88, 96, 99, 100, 102, 107, 108, 112, 117, 120, 126, 140, 159, 166, 174, 185, 199, 216, 261 }, 18);

            db.SaveChanges();
        }

        public void CourseNineGroups(APIDbContext db)
        {
            var reviews = db.TextReviews.Where(n => n.Course == 9).ToArray();

            CombineIntoOneGroup(reviews, new[] { 1, 2, 3, 4 }, 1);
            CombineIntoOneGroup(reviews, new[] { 5, 6 }, 2);
            CombineIntoOneGroup(reviews, new[] { 7, 9 }, 3);
            CombineIntoOneGroup(reviews, new[] { 8, 10 }, 4);
            CombineIntoOneGroup(reviews, new[] { 11, 12 }, 5);
            CombineIntoOneGroup(reviews, new[] { 13, 14 }, 6);
            CombineIntoOneGroup(reviews, new[] { 15, 16 }, 7);
            CombineIntoOneGroup(reviews, new[] { 17, 18 }, 8);
            CombineIntoOneGroup(reviews, new[] { 19, 20, 22 }, 9);
            CombineIntoOneGroup(reviews, new[] { 21, 23 }, 10);
            CombineIntoOneGroup(reviews, new[] { 24, 25, 26 }, 11);
            CombineIntoOneGroup(reviews, new[] { 27, 28, 29, 30 }, 12);
            CombineIntoOneGroup(reviews, new[] { 31, 32, 33, 34, 35 }, 13);
            CombineIntoOneGroup(reviews, new[] { 36, 37, 38, 39, 40, 41 }, 14);
            CombineIntoOneGroup(reviews, new[] { 42, 43, 44, 45, 46, 47, 48, 49 }, 15);
            CombineIntoOneGroup(reviews, new[] { 50, 51, 52, 53, 54, 56, 57, 58, 59, 60 }, 16);
            CombineIntoOneGroup(reviews, new[] { 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75 }, 17);
            CombineIntoOneGroup(reviews, new[] { 76, 77, 78, 79, 80, 82, 87, 89, 90, 97, 101, 102, 103, 110, 114, 116, 117, 120, 130, 131, 134, 135, 139, 148, 151, 201, 202, 217 }, 18);

            db.SaveChanges();
        }

        public void CourseTenGroups(APIDbContext db)
        {
            var reviews = db.TextReviews.Where(n => n.Course == 10).ToArray();

            CombineIntoOneGroup(reviews, new[] { 3, 4 }, 1);
            CombineIntoOneGroup(reviews, new[] { 5, 7 }, 2);
            CombineIntoOneGroup(reviews, new[] { 2, 6 }, 3);
            CombineIntoOneGroup(reviews, new[] { 8, 10 }, 4);
            CombineIntoOneGroup(reviews, new[] { 9, 11 }, 5);
            CombineIntoOneGroup(reviews, new[] { 12, 13 }, 6);
            CombineIntoOneGroup(reviews, new[] { 14, 15 }, 7);
            CombineIntoOneGroup(reviews, new[] { 16, 17 }, 8);
            CombineIntoOneGroup(reviews, new[] { 18, 19, 20 }, 9);
            CombineIntoOneGroup(reviews, new[] { 21, 22, 23, 24 }, 10);
            CombineIntoOneGroup(reviews, new[] { 25, 26, 27, 28 }, 11);
            CombineIntoOneGroup(reviews, new[] { 29, 30, 31, 32 }, 12);
            CombineIntoOneGroup(reviews, new[] { 33, 34, 35, 36, 37, 38 }, 13);
            CombineIntoOneGroup(reviews, new[] { 39, 40, 41, 42, 43, 44, 45 }, 14);
            CombineIntoOneGroup(reviews, new[] { 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56 }, 15);
            CombineIntoOneGroup(reviews, new[] { 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67 }, 16);
            CombineIntoOneGroup(reviews, new[] { 69, 70, 71, 72, 75, 76, 78, 81, 86, 88, 90, 91, 93, 95, 98, 99 }, 17);
            CombineIntoOneGroup(reviews, new[] { 101, 102, 103, 104, 106, 107, 108, 111, 112, 117, 122, 126, 134, 155, 161, 209, 212, 254 }, 18);

            db.SaveChanges();
        }

        public void CourseElevenGroups(APIDbContext db)
        {
            var reviews = db.TextReviews.Where(n => n.Course == 11).ToArray();

            CombineIntoOneGroup(reviews, new[] { 2, 3, 4 }, 1);
            CombineIntoOneGroup(reviews, new[] { 5, 6 }, 2);
            CombineIntoOneGroup(reviews, new[] { 7, 8 }, 3);
            CombineIntoOneGroup(reviews, new[] { 9, 11 }, 4);
            CombineIntoOneGroup(reviews, new[] { 10, 12 }, 5);
            CombineIntoOneGroup(reviews, new[] { 13, 14 }, 6);
            CombineIntoOneGroup(reviews, new[] { 15, 16 }, 7);
            CombineIntoOneGroup(reviews, new[] { 17, 18, 19 }, 8);
            CombineIntoOneGroup(reviews, new[] { 20, 21 }, 9);
            CombineIntoOneGroup(reviews, new[] { 22, 23, 24 }, 10);
            CombineIntoOneGroup(reviews, new[] { 25, 26, 27 }, 11);
            CombineIntoOneGroup(reviews, new[] { 28, 29, 30, 31 }, 12);
            CombineIntoOneGroup(reviews, new[] { 32, 33, 34, 35 }, 13);
            CombineIntoOneGroup(reviews, new[] { 36, 37, 38, 39, 40, 41, 42 }, 14);
            CombineIntoOneGroup(reviews, new[] { 43, 44, 45, 46, 47, 48, 49 }, 15);
            CombineIntoOneGroup(reviews, new[] { 50, 51, 52, 53, 55, 56, 57, 58, 59, 60, 61, 62, 63, 65, 66, 67 }, 16);
            CombineIntoOneGroup(reviews, new[] { 68, 70, 71, 72, 73, 75, 76, 77, 78, 79, 80, 81, 82, 84, 85, 86, 87, 92, 93, 95, 99, 104, 107, 111, 114, 117, 120, 137, 140, 165 }, 17);

            db.SaveChanges();
        }

        public void CourseTwelveGroups(APIDbContext db)
        {
            var reviews = db.TextReviews.Where(n => n.Course == 12).ToArray();

            CombineIntoOneGroup(reviews, new[] { 1, 2, 3 }, 1);
            CombineIntoOneGroup(reviews, new[] { 4, 5 }, 2);
            CombineIntoOneGroup(reviews, new[] { 6, 7 }, 3);
            CombineIntoOneGroup(reviews, new[] { 8, 9 }, 4);
            CombineIntoOneGroup(reviews, new[] { 10, 11 }, 5);
            CombineIntoOneGroup(reviews, new[] { 12, 13 }, 6);
            CombineIntoOneGroup(reviews, new[] { 14, 15 }, 7);
            CombineIntoOneGroup(reviews, new[] { 16, 17 }, 8);
            CombineIntoOneGroup(reviews, new[] { 18, 19, 20 }, 9);
            CombineIntoOneGroup(reviews, new[] { 21, 22, 23 }, 10);
            CombineIntoOneGroup(reviews, new[] { 24, 25, 26 }, 11);
            CombineIntoOneGroup(reviews, new[] { 27, 28, 29, 30 }, 12);
            CombineIntoOneGroup(reviews, new[] { 31, 32, 33, 34, 35 }, 13);
            CombineIntoOneGroup(reviews, new[] { 36, 37, 38, 39, 40, 41, 42 }, 14);
            CombineIntoOneGroup(reviews, new[] { 43, 44, 45, 46, 47, 48, 49 }, 15);
            CombineIntoOneGroup(reviews, new[] { 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65 }, 16);
            CombineIntoOneGroup(reviews, new[] { 66, 67, 68, 69, 70, 72, 73, 75, 77, 78, 80 }, 17);
            CombineIntoOneGroup(reviews, new[] { 82, 83, 85, 86, 87, 88, 89, 93, 94, 95, 99, 100, 103, 105, 107, 108, 111, 113, 116, 117, 121, 124, 148, 154, 165, 196, 219, 273 }, 18);

            db.SaveChanges();
        }

        public void CourseThirteenGroups(APIDbContext db)
        {
            var reviews = db.TextReviews.Where(n => n.Course == 13).ToArray();

            CombineIntoOneGroup(reviews, new[] { 1, 2, 3, 4 }, 1);
            CombineIntoOneGroup(reviews, new[] { 5, 6 }, 2);
            CombineIntoOneGroup(reviews, new[] { 7, 8 }, 3);
            CombineIntoOneGroup(reviews, new[] { 9, 10 }, 4);
            CombineIntoOneGroup(reviews, new[] { 11, 12 }, 5);
            CombineIntoOneGroup(reviews, new[] { 13, 14 }, 6);
            CombineIntoOneGroup(reviews, new[] { 15, 16 }, 7);
            CombineIntoOneGroup(reviews, new[] { 17, 18, 19 }, 8);
            CombineIntoOneGroup(reviews, new[] { 20, 21, 22 }, 9);
            CombineIntoOneGroup(reviews, new[] { 23, 24, 25 }, 10);
            CombineIntoOneGroup(reviews, new[] { 26, 27, 28, 29 }, 11);
            CombineIntoOneGroup(reviews, new[] { 30, 31, 32, 33 }, 12);
            CombineIntoOneGroup(reviews, new[] { 34, 35, 36, 37, 38, 39 }, 13);
            CombineIntoOneGroup(reviews, new[] { 40, 41, 42, 43, 44, 45, 46, 47 }, 14);
            CombineIntoOneGroup(reviews, new[] { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57 }, 15);
            CombineIntoOneGroup(reviews, new[] { 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68 }, 16);
            CombineIntoOneGroup(reviews, new[] { 69, 70, 73, 74, 75, 77, 79, 80, 81, 82, 83, 89, 91, 92, 93, 94, 97, 98, 100, 106, 112, 114, 126, 140, 142, 182, 189, 216, 225, 243, 632 }, 17);

            db.SaveChanges();
        }

        private void CombineIntoOneGroup(TextReviews[] reviews, int[] counts, int group)
        {
            reviews.Where(n => counts.Contains(Utility.WordCount(n.Review))).ToList().ForEach(n => n.Grouping = group);
        }

        private void SplitIntoGroups(TextReviews[] reviews, int count, int[] groups)
        {
            var revs = reviews.Where(n => Utility.WordCount(n.Review) == count).ToArray();
            revs = revs.OrderBy(n => rng.Next()).ToArray();
            var amount = revs.Length / groups.Length;
            if (revs.Length % groups.Length != 0)
                amount++;

            for (int i = 0; i < groups.Length; i++)
            {
                revs.Skip(i * amount).Take(amount).ToList().ForEach(n => n.Grouping = groups[i]);
            }
        }
    }
}
